using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XClipboard
{
    public partial class Form1 : Form
    {
        private Image lasteContent = null;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 判断图片是否一致
        /// </summary>
        /// <param name="img">图片一</param>
        /// <param name="bmp">图片二</param>
        /// <returns>是否一致</returns>
        public static bool IsSameImg(Bitmap img, Bitmap bmp)
        {
            if (bmp == null || img == null) return false;
            //大小一致
            if (img.Width == bmp.Width && img.Height == bmp.Height)
            {
                //将图片一锁定到内存
                BitmapData imgData_i = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_i = imgData_i.Scan0;
                int length_i = imgData_i.Width * imgData_i.Height * 3;
                byte[] imgValue_i = new byte[length_i];
                Marshal.Copy(ipr_i, imgValue_i, 0, length_i);
                img.UnlockBits(imgData_i);
                //将图片二锁定到内存
                BitmapData imgData_b = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_b = imgData_b.Scan0;
                int length_b = imgData_b.Width * imgData_b.Height * 3;
                byte[] imgValue_b = new byte[length_b];
                Marshal.Copy(ipr_b, imgValue_b, 0, length_b);
                img.UnlockBits(imgData_b);
                //长度不相同
                if (length_i != length_b)
                {
                    return false;
                }
                else
                {
                    //循环判断值
                    for (int i = 0; i < length_i; i++)
                    {
                        //不一致
                        if (imgValue_i[i] != imgValue_b[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else
                return false;
        }
        /// <summary>
        /// 连接数据库文件
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private bool ConnectSqlite(SqliteConnection connection)
        {
            try
            {
                connection.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = connection;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS AllData(Id INTEGER PRIMARY KEY AUTOINCREMENT,Time TEXT,Fromm TEXT,Type TEXT,Content TEXT)";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                if (connection.ConnectionString.Contains("Password"))
                    MessageBox.Show("连接数据库失败，可能您的数据库设置了密码，请确保您的密码正确后重试。\r\n" +
                        e.Message, "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("数据库连接失败，请重试。\r\n" +
                        e.Message, "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 窗口关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sharpclipboard.StopMonitoring();
        }
        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            sharpclipboard.StartMonitoring();
            sharpclipboard.ClipboardChanged += Sharpclipboard_ClipboardChanged;
            dataGridView.RowHeadersVisible = false;
        }
        /// <summary>
        /// 打开数据库文件
        /// </summary>
        /// <returns></returns>
        private SqliteConnection OpenSqlite()
        {
            Directory.CreateDirectory("Data\\");
            var connStr = @"Data Source=Data\\TestSqlite.db";//连接字符串
            var conn = new SqliteConnectionStringBuilder(connStr)
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                //Password = "password"
            }.ToString();//使用这个方式设置密码，避免sql注入
            return new SqliteConnection(conn);//创建SQLite连接
        }
        /// <summary>
        /// 剪贴板监听事件，剪贴板信息改动时进入事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sharpclipboard_ClipboardChanged(object sender, WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.SourceApplication.Title == this.Text) return; // 不复制自己的内容

            ShowTip.BeginInvoke((MethodInvoker)delegate ()
            {
                switch (e.ContentType)
                {
                    case WK.Libraries.SharpClipboardNS.SharpClipboard.ContentTypes.Text:
                        ShowTip.Text = "剪贴板类型：文本类型\r\n\r\n";
                        ShowTip.Text += sharpclipboard.ClipboardText;
                        WriteSqlite("文本类型", sharpclipboard.ClipboardText, e.SourceApplication.Title);
                        break;

                    case WK.Libraries.SharpClipboardNS.SharpClipboard.ContentTypes.Image:
                        ShowTip.Text = "剪贴板类型：图片类型\r\n\r\n" +
                        "         =====>>      ";
                        Image img = sharpclipboard.ClipboardImage;
                        ShowPic.Image = img;
                        string picname = "Pic\\" + DateTime.Now.ToString("yyyy MM-dd HH-mm-ss") + ".png";
                        if (!Directory.Exists("Pic\\")) Directory.CreateDirectory("Pic\\");
                        using (FileStream f = new FileStream(picname, FileMode.Create))
                            img.Save(f, ImageFormat.Png);

                        WriteSqlite("图片类型", picname, e.SourceApplication.Title, e.Content);
                        break;

                    case WK.Libraries.SharpClipboardNS.SharpClipboard.ContentTypes.Files:
                        ShowTip.Text = "剪贴板类型：文件类型\r\n\r\n";
                        string txt = "";
                        if (sharpclipboard.ClipboardFiles.Count > 0)
                            foreach (var item in sharpclipboard.ClipboardFiles)
                            {
                                //获取文件大小 后缀 类型
                                FileInfo fileInfo = new FileInfo(item);
                                if (fileInfo.Attributes == FileAttributes.Directory)
                                {
                                    ShowTip.Text += "文件夹名：" + fileInfo.Name + "\r\n";
                                    ShowTip.Text += "文件夹路径：" + item + "\r\n\r\n";
                                }
                                else
                                {
                                    ShowTip.Text += "文件名：" + fileInfo.Name + "\r\n";
                                    ShowTip.Text += "文件大小：" + fileInfo.Length + " KB\r\n";
                                    ShowTip.Text += "文件路径：" + item + "\r\n\r\n";
                                }
                                txt += item + "\r\n";
                            }
                        WriteSqlite("文件类型", txt, e.SourceApplication.Title);
                        break;

                    default:
                        ShowTip.Text = "剪贴板类型：未知\r\n\r\n";
                        break;
                }
            });
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testsqlite_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var connection = OpenSqlite();
                // 创建一个表 记录 序号（主键、自增）时间（文本）类型内容(文本)
                if (connection.State != ConnectionState.Open)
                {
                    if (!ConnectSqlite(connection)) return;
                    SqliteCommand cmd = new SqliteCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS AllData(Id INTEGER PRIMARY KEY AUTOINCREMENT,Time TEXT,Fromm TEXT,Type TEXT,Content TEXT)";
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                Console.WriteLine("初始化数据库完毕");
            });
        }
        /// <summary>
        /// 写数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testsqlite2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var connection = OpenSqlite();
                if (connection.State != ConnectionState.Open)
                {
                    if (!ConnectSqlite(connection)) return;
                    SqliteCommand cmd = new SqliteCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO AllData(Time,Fromm,Type,Content) VALUES(@Time,@Fromm,@Type,@Content)";
                    cmd.Parameters.Add("@Time", SqliteType.Text).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("@Type", SqliteType.Text).Value = "文本类型";
                    cmd.Parameters.Add("@Content", SqliteType.Text).Value = $"测试信息";
                    cmd.Parameters.Add("@Fromm", SqliteType.Text).Value = $"测试程序";

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            });
        }
        /// <summary>
        /// 读取数据库，显示到表格中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testsqlite3_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                //读数据库
                var connection = OpenSqlite();
                if (connection.State != ConnectionState.Open)
                {
                    if (!ConnectSqlite(connection)) return;
                    SqliteCommand cmd = new SqliteCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT * FROM AllData";
                    SqliteDataReader reader = cmd.ExecuteReader();
                    dataGridView.Invoke(new Action(() =>
                    {
                        dataGridView.Rows.Clear();
                    }));

                    while (reader.Read())
                    {
                        ShowTip.Invoke(new Action(() =>
                        {
                            ShowTip.Text += "序号：" + reader["Id"] + "\r\n";
                            ShowTip.Text += "时间：" + reader["Time"] + "\r\n";
                            ShowTip.Text += "类型：" + reader["Type"] + "\r\n";
                            ShowTip.Text += "内容：" + reader["Content"] + "\r\n\r\n";
                        }));
                        dataGridView.Invoke(new Action(() =>
                        {
                            int index = this.dataGridView.Rows.Add();
                            dataGridView.Rows[index].Cells[0].Value = reader["Id"];
                            dataGridView.Rows[index].Cells[1].Value = reader["Type"];
                            dataGridView.Rows[index].Cells[2].Value = reader["Time"];
                            dataGridView.Rows[index].Cells[3].Value = reader["Content"];
                        }));
                    }
                }
                connection.Close();
            });
        }
        /// <summary>
        /// 硬删除数据库数据，删除db文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testsqlite4_Click(object sender, EventArgs e)
        {
            if (File.Exists("Data\\TestSqlite.db"))
            {
                try
                {
                    File.Delete("Data\\TestSqlite.db");
                }
                catch
                {
                    MessageBox.Show("数据库被占用，请重启程序后重试", "通知", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!File.Exists("Data\\TestSqlite.db"))
            {
                MessageBox.Show("数据库成功删除", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                testsqlite_Click(null, null);//初始化一个空的数据库
            }
        }
        /// <summary>
        /// 软清空数据库数据，清空库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testsqlite5_Click(object sender, EventArgs e)
        {
            var connection = OpenSqlite();
            //清空数据库内容
            if (connection.State != ConnectionState.Open)
            {
                if (!ConnectSqlite(connection)) return;
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = connection;
                cmd.CommandText = "delete from AllData";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "update sqlite_sequence SET seq = 0 where name ='AllData'";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            MessageBox.Show("数据库表已清空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 写数据库数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="From"></param>
        /// <param name="eContent"></param>
        private void WriteSqlite(string type, string text, string From, object eContent = null)
        {
            if (eContent != null)
            {
                if (IsSameImg((Bitmap)eContent, (Bitmap)lasteContent)) return;
                lasteContent = (Image)eContent;
            }
            Task.Run(() =>
            {
                var connection = OpenSqlite();
                if (connection.State != ConnectionState.Open)
                {
                    if (!ConnectSqlite(connection)) return;
                    SqliteCommand cmd = new SqliteCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO AllData(Time,Fromm,Type,Content) VALUES(@Time,@Fromm,@Type,@Content)";
                    cmd.Parameters.Add("@Time", SqliteType.Text).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("@Type", SqliteType.Text).Value = type;
                    cmd.Parameters.Add("@Content", SqliteType.Text).Value = text;
                    cmd.Parameters.Add("@Fromm", SqliteType.Text).Value = From;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            });
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() == "图片类型") //第三列 剪贴板内容列
            {
                //打开图片
                Process.Start(dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
        }

    }
}