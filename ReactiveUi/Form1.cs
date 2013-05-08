using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ReactiveUi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => textBox1.Text = DateTime.Now.ToLongTimeString());

            var textChanged = Observable.FromEventPattern<EventHandler, EventArgs>
             (
              handler => handler.Invoke,
              h => textBox3.TextChanged += h,
              h => textBox3.TextChanged -= h
             );

            textChanged.Subscribe(_ => textBox2.Text =
            textBox3.Text
            .Split()
            .DefaultIfEmpty()
            .Where(s => s.Trim().Length > 0)
            .Count()
            .ToString());
        }

    }
}
