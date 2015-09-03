using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.Win32;
using ToolBox.Encry;

namespace ToolBox.Pages
{
    /// <summary>
    /// Interaction logic for EncryAssembly.xaml
    /// </summary>
    public partial class EncryAssembly : UserControl
    {
        public EncryAssembly()
        {
            InitializeComponent();
        }

        private string openFilePath = "";
        private string EncryKey = "";
        private string UnEncryKey = "";
        private Assembly asm;

        private void Button_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                openFilePath = tb_AssemblyPath.Text = ofd.FileName;
            }
        }
 
        private void Button_EncryAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_AssemblyPath.Text) || string.IsNullOrEmpty(tb_encryKey.Text))
                return;
            
            EncryKey=tb_encryKey.Text;
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                try
                {
                    byte[] assemblyByteData = File.ReadAllBytes(openFilePath);
                    byte[] encryBytes = ByteEncry.EncryptionToBytes(assemblyByteData, EncryKey);
                    string dirPath = Path.GetDirectoryName(openFilePath);
                    File.WriteAllBytes(Path.Combine(dirPath, "加密后的程序集.dll"), encryBytes);

                    MessageBox.Show("加密成功");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));
        }


        private void Button_UnEncryAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_UnEncryKey.Text))
                return;

            UnEncryKey = tb_UnEncryKey.Text;
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                try
                {
                    byte[] hadEncryBytes = File.ReadAllBytes(openFilePath);
                    byte[] assemblyByteData = ByteEncry.UnEncryptionToBytes(hadEncryBytes, UnEncryKey);

                    string dirPath = Path.GetDirectoryName(openFilePath);
                    File.WriteAllBytes(Path.Combine(dirPath, "解密后的程序集.dll"), assemblyByteData);

                    asm = Assembly.Load(assemblyByteData);
                    Type[] types = asm.GetTypes();

                    this.Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        TypeList.Items.Clear();
                        foreach (var type in types)
                        {
                            TypeList.Items.Add(type.FullName);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));
        }
 
        private void Button_CreateType_Click(object sender, RoutedEventArgs e)
        {
            if (asm == null)
                return;
            string typeName = TypeList.SelectedValue.ToString();
            object obj = asm.CreateInstance(typeName);
            if (obj == null)
                return;

            MethodInfo[] methods =  obj.GetType().GetMethods();
            MethodList.Items.Clear();
            foreach (var method in methods)
            {
                MethodList.Items.Add(method.Name);
            }
        }
    }
}
