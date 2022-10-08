using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    internal class Program
    {
        static ComboBox cbBox = new ComboBox();
        static ComboBox cbBoxCal = new ComboBox();
        static Label lb = new Label();
        static List<TextBox> myTextboxList = new List<TextBox>();
        static TextBox[,] tbLeft = new TextBox[5, 5];
        static TextBox[,] tbRight = new TextBox[5, 5];
        static TextBox[,] tbResult = new TextBox[5, 5];

        static int row = 2;
        static int col = 2;
        static int _row = 2;
        static int _col = 2;
        static Panel panel = new Panel();
        static FlowLayoutPanel flowLayoutPanelLeft = new FlowLayoutPanel();
        static FlowLayoutPanel flowLayoutPanelRight = new FlowLayoutPanel();
        static FlowLayoutPanel flowLayoutPanelResult = new FlowLayoutPanel();

        static Button btn_equal = new Button();

        static int Location_x = 10;
        static int Location_y = 10;
        static void Main(string[] args)
        {
            Form mainForm = new Form();
            mainForm.Text = "Matrix";
            mainForm.Width = 1000;
            mainForm.Height = 750;
           //mainForm.AutoSize = true;

            lb.Text = "Kích Thước";
            lb.Font = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Point);
            lb.Width = 500;
            lb.Height = 50;
            lb.Size = new Size(500, 50);
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.BackColor = Color.Blue;
            lb.Location = new System.Drawing.Point(0, 0);
            mainForm.Controls.Add(lb);


            for (int i = 2; i < 6; i++)
            {
                cbBox.Items.Add(i);
            }
            cbBox.SelectedIndex = 0;
            cbBox.Height = 100;
            cbBox.Size = new Size(100, 50);
            cbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBox.Location = new System.Drawing.Point(lb.Width + 100, 0);
            cbBox.SelectedIndexChanged += new EventHandler(cbox_changed_index);
            mainForm.Controls.Add(cbBox);

            panel.BackColor = Color.Gray;
            panel.Width = mainForm.Width;
            panel.Height = mainForm.Height;
            panel.Location = new System.Drawing.Point(0, 100);
            panel.AutoSize = true;
            mainForm.Controls.Add(panel);

            flowLayoutPanelLeft.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelLeft.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 10);
            flowLayoutPanelLeft.BackColor = Color.White;
            flowLayoutPanelLeft.Left = 10;
            flowLayoutPanelLeft.Top = 10;
            panel.Controls.Add(flowLayoutPanelLeft);

            cbBoxCal.Location = new Point(0, 0);
            cbBoxCal.Width = 50;
            cbBoxCal.Height = 50;
            cbBoxCal.Size = new Size(50, 1000);
            cbBoxCal.Items.Add('+');
            cbBoxCal.Items.Add('*');
            cbBoxCal.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoxCal.SelectedIndex = 0;
            cbBoxCal.Left = flowLayoutPanelLeft.Left + 50 + panel.Width / 3;
            cbBoxCal.Top = 10;
            panel.Controls.Add(cbBoxCal);

            flowLayoutPanelRight.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelRight.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 10);
            flowLayoutPanelRight.BackColor = Color.White;
            flowLayoutPanelRight.Left = (5 + 1) * 10 + 5 * 50 + 100 + panel.Width / 6 * 2;
            flowLayoutPanelRight.Top = 10;
            panel.Controls.Add(flowLayoutPanelRight);

            btn_equal.Text = "=";
            btn_equal.Size = new Size(100, 50);
            btn_equal.Font = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Point);
            btn_equal.BackColor = Color.White;
            btn_equal.Left = panel.Width / 3 + 30;
            btn_equal.Top = panel.Height / 4;
            btn_equal.Click += new EventHandler(btn_equal_click);
            panel.Controls.Add(btn_equal);

            flowLayoutPanelResult.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelResult.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 10);
            flowLayoutPanelResult.BackColor = Color.White;
            flowLayoutPanelResult.Left = panel.Width / 3;
            flowLayoutPanelResult.Top = panel.Height / 3;
            panel.Controls.Add(flowLayoutPanelResult);

            for (int i = 0; i < row; i++) 
            {
                for (int j = 0; j < col; j++) 
                {
                    tbLeft[i, j] = new TextBox();
                    tbLeft[i, j].Text = "0";
                    tbLeft[i, j].Size = new System.Drawing.Size(50, 50);
                    flowLayoutPanelLeft.Controls.Add(tbLeft[i, j]);
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tbRight[i, j] = new TextBox();
                    tbRight[i, j].Text = "0"; 
                    tbRight[i, j].Size = new Size(50, 50);
                    flowLayoutPanelRight.Controls.Add(tbRight[i, j]);
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tbResult[i, j] = new TextBox();
                    tbResult[i, j].Text = "0";
                    tbResult[i, j].Size = new Size(50, 50);
                    flowLayoutPanelResult.Controls.Add(tbResult[i, j]);
                }
            }

            


            mainForm.Show();
            Application.Run(mainForm);
        }
        static void cbox_changed_index(object sender, EventArgs e)
        {
            _row = Convert.ToInt16(cbBox.Text);
            _col = _row;
            removeArray(row, col);
            createArray(_row, _col);
            row = _row;
            col = _col;
        }
        static void removeArray(int row, int col)
        {
            for (int i = 0; i < row; i++) 
            {
                for (int j = 0; j < col; j++) 
                {
                    flowLayoutPanelLeft.Controls.Remove(tbLeft[i, j]);
                    tbLeft[i, j].Dispose();

                    flowLayoutPanelRight.Controls.Remove(tbRight[i, j]);
                    tbRight[i, j].Dispose();

                    flowLayoutPanelResult.Controls.Remove(tbResult[i, j]);
                    tbResult[i, j].Dispose();
                }
            }
        }
        static void createArray(int row, int col)
        {
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tbLeft[i, j] = new TextBox();
                    tbLeft[i, j].Text = "0";
                    tbLeft[i, j].Size = new Size(50, 50);
                    tbLeft[i, j].Location = new System.Drawing.Point(Location_x += 10, Location_y += 10);
                    flowLayoutPanelLeft.Controls.Add(tbLeft[i, j]);
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tbRight[i, j] = new TextBox();
                    tbRight[i, j].Text = "0";
                    tbRight[i, j].Size = new Size(50, 50);
                    tbRight[i, j].Location = new System.Drawing.Point(Location_x += 10, Location_y += 10);
                    flowLayoutPanelRight.Controls.Add(tbRight[i, j]);
                }
            }
            for(int i=0;i<row;i++)
            {
                for(int j= 0;j<col;j++)
                {
                    tbResult[i, j] = new TextBox();
                    tbResult[i, j].Text = "0";
                    tbResult[i, j].Size = new Size(50, 50);
                    tbResult[i, j].Location = new System.Drawing.Point(Location_x += 10, Location_y += 10);
                    flowLayoutPanelResult.Controls.Add(tbResult[i, j]);
                }
            }
            updateSizeFlowPanel(row);
        }
        static void updateSizeFlowPanel(int row)
        {
            flowLayoutPanelLeft.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 15);
            flowLayoutPanelRight.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 15);
            flowLayoutPanelResult.Size = new Size((row + 1) * 10 + row * 50, (row + 1) * 10 + row * 15);

            flowLayoutPanelRight.Left = (5 + 1) * 10 + 5 * 50 + panel.Width / 6 * 2;
        }
        static int Mul_row_col(int i, int j)
        {
            return int.Parse(tbLeft[i, j].Text) * int.Parse(tbRight[i, j].Text)
                + int.Parse(tbLeft[i + 1, j].Text) * int.Parse(tbRight[i,j+1].Text)
                + int.Parse(tbLeft[i+2,j].Text) * int.Parse(tbRight[i, j+2].Text);
        }
        static void btn_equal_click(object sender, EventArgs e)
        {
            if(cbBoxCal.Text == "+")
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        tbResult[i, j].Text = (int.Parse(tbLeft[i, j].Text) + int.Parse(tbRight[i, j].Text)).ToString();
                    }
                }
            }
            else if(cbBoxCal.Text == "*")
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        int temp = 0;
                        for (int k = 0; k < row; k++)
                        {
                            temp += int.Parse(tbLeft[i, k].Text) * int.Parse(tbRight[k, j].Text);
                        }
                        tbResult[i, j].Text = temp.ToString();
                    }
                }
            }
            
        }
    }
}