using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace RegAsmGui
{
    public partial class Form1 : Form
    {
        private string[] filePaths;

        public Form1()
        {
            InitializeComponent();
            FillList();
        }


        private void FillList()
        {
            string path = Directory.GetCurrentDirectory();
            filePaths = Directory.GetFiles(path, "*.dll");
            listBox1.Items.AddRange(filePaths);
        }

        private void Main()
        {

            foreach (var filePath in filePaths)
            {
                bool bResult = true;
                try
                {
                    Assembly asm = Assembly.LoadFile(filePath);
                    RegistrationServices regAsm = new RegistrationServices();

                    bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);
                    Console.WriteLine(filePath + " Register: " + bResult);
                    // Console.WriteLine(asm.FullName);
                }
                catch (Exception e)
                {

                    Console.WriteLine(filePath + " " + e.Message);
                }
            }
            Console.ReadLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = listBox1.SelectedItems;
            foreach (var dll in s)
            {
                if (!listBox2.Items.Contains(dll))
                {

                    listBox2.Items.Add(dll);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            List<string> removals = new List<string>();
            foreach (string s in listBox2.SelectedItems)
            {

                removals.Add(s);
            }

            foreach (var dll in removals)
            {
                listBox2.Items.Remove(dll);
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (rbtReg.Checked)
            {
                Register();
            }
            else if (rbtUnReg.Checked)
            {
                UnRegister();

            }
           
        }

        private void UnRegister()
        {
            listBox3.Items.Clear();
            bool bResult = true;
            var dlls = listBox2.Items;
            if (dlls.Count < 1) return;
            foreach (var dll in dlls)
            {
                try
                {



                    Assembly asm = Assembly.LoadFile(dll.ToString());
                    RegistrationServices regAsm = new RegistrationServices();

                    bResult = regAsm.UnregisterAssembly(asm);
                    listBox3.Items.Add(dll + "Un  Register: " + bResult);
                    // Console.WriteLine(asm.FullName);
                }

                catch (Exception ex)
                {

                    listBox3.Items.Add("Error " + dlls + " " + ex.Message);
                }
            }
        }

        private void Register()
        {
            listBox3.Items.Clear();
            bool bResult = true;
            var dlls = listBox2.Items;
            if (dlls.Count < 1) return;
            foreach (var dll in dlls)
            {
                try
                {



                    Assembly asm = Assembly.LoadFile(dll.ToString());
                    RegistrationServices regAsm = new RegistrationServices();

                    bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);
                    listBox3.Items.Add(dll + " Register: " + bResult);
                    // Console.WriteLine(asm.FullName);
                }

                catch (Exception ex)
                {

                    listBox3.Items.Add("Error " + dlls + " " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtReg_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtReg.Checked)
            {
                btnReg.Text = "Register";
            }
            else if (rbtUnReg.Checked)
            {
                btnReg.Text = "Un Register";

            }
            else
            { btnReg.Text = ""; }

        }
    }
}
