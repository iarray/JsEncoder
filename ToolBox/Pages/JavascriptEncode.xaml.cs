using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Dean.Edwards;
using Microsoft.Win32;
using ToolBox.Model;

namespace ToolBox.Pages
{
    /// <summary>
    /// Interaction logic for Javascript加密.xaml
    /// </summary>
    public partial class Javascript加密 : UserControl,IDisposable
    {
        private ObservableCollection<FileModel> Files = new ObservableCollection<FileModel>();
        private List<Thread> EncodingThreadList = new List<Thread>();
        private int NowFileIndex = 0;
        private Encoding FileEncoding = Encoding.Default;
        private string OutPutDirPath = "";
        private List<string> FailureFileList = new List<string>();
        private bool isFastCode = true;
        private bool isSpecialCharacters = false;
        private ECMAScriptPacker.PackerEncoding PackerEncoding = ECMAScriptPacker.PackerEncoding.Normal;

        public Javascript加密()
        {
            InitializeComponent();

            fileList.DataContext = this;
            fileList.ItemsSource = Files;
            
            SetEncodingWay();
            SetEncodeType();
        }

        ~Javascript加密()
        {
            StopAllThread();
        }

        private void StopAllThread()
        {
            for (int i = 0; i < EncodingThreadList.Count; ++i)
            {
                EncodingThreadList[i].Abort();
            }
        }

        public void SetEncodingWay()
        {
            EncodingWay.Items.Add(ECMAScriptPacker.PackerEncoding.None);
            EncodingWay.Items.Add(ECMAScriptPacker.PackerEncoding.Numeric);
            EncodingWay.Items.Add(ECMAScriptPacker.PackerEncoding.Mid);
            EncodingWay.Items.Add(ECMAScriptPacker.PackerEncoding.Normal);
            EncodingWay.Items.Add(ECMAScriptPacker.PackerEncoding.HighAscii);
            EncodingWay.SelectedItem = ECMAScriptPacker.PackerEncoding.Normal;
        }

        public void SetEncodeType()
        {
            EncodeType.Items.Add("ASCII");
            EncodeType.Items.Add("UTF8");
            EncodeType.Items.Add("Unicode");
            EncodeType.Items.Add("UTF7");
            EncodeType.Items.Add("UTF32");
            EncodeType.Items.Add("Default");
            EncodeType.SelectedItem = "UTF8";
        }

        public Encoding GetEncoding(string EncodingName)
        {
            if (EncodingName == "ASCII")
                return Encoding.ASCII;
            else if (EncodingName == "UTF8")
                return Encoding.UTF8;
            else if (EncodingName == "UTF7")
                return Encoding.UTF7;
            else if (EncodingName == "UTF32")
                return Encoding.UTF32;
            else if (EncodingName == "Unicode")
                return Encoding.Unicode;
            else
                return Encoding.Default;

        }

        private void btn_add_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                foreach (string fPath in ofd.FileNames)
                {
                    Files.Add(new FileModel(Path.GetFileName(fPath), fPath,Path.GetFileName(fPath)));
                }
            }
        }

        private void btn_add_dir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            SearchOption searchOption = ScanChildDir.IsChecked ==true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filePathArray;
                ProgressBar.Visibility = Visibility.Visible;
                ProgressBar.IsIndeterminate = true;
                ThreadPool.QueueUserWorkItem( (obj)=>{
                    filePathArray = Directory.GetFiles(fbd.SelectedPath, "*.js", searchOption);

                    this.Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        foreach (string filePath in filePathArray)
                        {
                                Files.Add(new FileModel(Path.GetFileName(filePath), filePath,filePath.Replace(fbd.SelectedPath,"")));
                        

                        }
                        ProgressBar.Visibility = Visibility.Hidden;
                    }), null);
                });
                
                
            }
        }

        private void btn_removeAll_Click(object sender, RoutedEventArgs e)
        {
            Files.Clear();
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            int fileSelectIndex = fileList.SelectedIndex;
            if (fileSelectIndex < 0)
                return;
            Files.RemoveAt(fileSelectIndex);
        }

        private void btn_btn_protect_Click(object sender, RoutedEventArgs e)
        {
            if (Files.Count <= 0 || string.IsNullOrEmpty(ThreadCount.Text) )
                return;
            int threadCount = int.Parse(ThreadCount.Text);
            FileEncoding = GetEncoding(EncodeType.SelectedValue.ToString());
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "请选择输出目录";
            fbd.ShowNewFolderButton = true;
            isFastCode = FastDecode.IsChecked == null ? false : (bool)FastDecode.IsChecked;
            isSpecialCharacters = SpecialCharacters.IsChecked == null ? false : (bool)SpecialCharacters.IsChecked;
            PackerEncoding = (ECMAScriptPacker.PackerEncoding)EncodingWay.SelectedItem;
            
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FailureFileList.Clear();
                OutPutDirPath = fbd.SelectedPath;
                ProgressBar.IsIndeterminate = false;
                ProgressBar.Visibility = Visibility.Visible;
                ProgressBar.Maximum = double.Parse(FileCount.Text);
                ProgressBar.Minimum = 0;

                StopAllThread();
                EncodingThreadList.Clear();

                for (int i = 0; i < threadCount; ++i)
                {
                    Thread encodingThread = new Thread(new ParameterizedThreadStart(encodingHandler));
                    encodingThread.IsBackground = true;
                    EncodingThreadList.Add(encodingThread);
                    encodingThread.Start(encodingThread);
                }
            }
        }
          
        private void ThreadCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void encodingHandler(object thread)
        {
            while (true)
            {
                int processingIndex = NowFileIndex;
                lock (this)
                {
                    processingIndex = NowFileIndex;
                    ++NowFileIndex;
                }
                if (processingIndex >= Files.Count)
                    ((Thread)thread).Abort();
                try
                {
                    using (StreamReader fileReader = new StreamReader(Files[processingIndex].Path, FileEncoding))
                    {
                        string jsCode = fileReader.ReadToEnd();

                        ECMAScriptPacker encoder = new ECMAScriptPacker(PackerEncoding, isFastCode, isSpecialCharacters);
                        string encodeJs = encoder.Pack(jsCode).Replace("\n", "\r\n");
                        string outPutFilePath = Path.Combine(OutPutDirPath, Files[processingIndex].OutputDir.Substring(1));
                        string outPutFileDirPath = Path.GetDirectoryName(outPutFilePath);

                        if (!Directory.Exists(outPutFileDirPath))
                            Directory.CreateDirectory(outPutFileDirPath);

                        using (StreamWriter fileWriter = new StreamWriter(outPutFilePath, true, FileEncoding))
                        {
                            fileWriter.Write(encodeJs);
                        }
                    }
                    IncreaseProgress(processingIndex);
                }
                catch
                {
                    FailureFileList.Add(Files[processingIndex].Path);

                    this.Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        ++ProgressBar.Value;
                    }));
                }
            }
        }

        public void WriteFailureLog()
        {
            using (StreamWriter fileWriter = new StreamWriter(OutPutDirPath + "\\log.txt", false))
            {
                fileWriter.WriteLine("/*******************************************************/");
                fileWriter.WriteLine("/*            " + DateTime.Now.ToString() + " 失败文件列表            */");
                fileWriter.WriteLine("/*******************************************************/");
                for (int i = 0; i < FailureFileList.Count; ++i)
                {
                    fileWriter.WriteLine(FailureFileList[i]);
                }
            }
        }


        public void IncreaseProgress(int processingIndex)
        {
            this.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                ++ProgressBar.Value;
                Files[processingIndex].FileEncodeSate = EncodeSate.已加密;
                if (ProgressBar.Value >= ProgressBar.Maximum)
                {
                    MessageBox.Show("加密完成,其中失败" + FailureFileList.Count + "个");
                    WriteFailureLog();
                }
            }), null);
        }

        public void Dispose()
        {
            StopAllThread();
        }
    }
}
