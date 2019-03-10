using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*Current BUGS : Program does not display values on first click (found on 5/10/2016)
 * txtTableTemp and txtTableDollaris not going to next line (found on 5/25/2016)
 */


namespace Assessment1
{
    public partial class PoolCal : Form
    {
        const float AVGTEMPLOWER = 5;
        const float AVGTEMPUPPER = 23;
        const float AVGTEMPSTEP = 1.5F;
        const long HEATINGADJUSTMENT = 32500;
        const int TARGETTEMP = 25;
        
        const int LENGTHLOWER = 5;
        const int LENGTHUPPER = 50;
        
        const int WIDTHLOWER = 2;
        const int WIDTHUPPER = 20;

        const int AVGDEPTHLOWER = 2;
        const int AVGDEPTHUPPER = 4;

        const long SMALLPOOLUPPER = 500000;
        const long MEDIUMPOOLUPPER = 1500000;

        float Length;
        float Width;
        float AvgDepth;
        float Volume;
        float SurfaceArea;
       
        public PoolCal()
        {
            InitializeComponent();
        }
        
       
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            float Width = float.Parse(txtWidth.Text);            
            float Length = float.Parse(txtLength.Text);            
            float AvgDepth = float.Parse(txtAvgDepth.Text);
           

            // if Width is NOT valid, this will return the ValidWidth method.
            if (!(ValidWidth(float.Parse(txtWidth.Text))))
            {
                return;
            }
             //if Length is NOT valid, this will return the ValidLength method.           
            if (!(ValidLength(float.Parse(txtLength.Text))))
            {
                return;
            }
            //if Average Depth is NOT valid, this will return the ValidAvgDepth method.
            if (!(ValidAvgDepth(float.Parse(txtAvgDepth.Text))))
            {
                return;
            }

            // call's methods to find out the Volume,Surface Area, and size of pool and displays them to their '.Text' boxes. 
            CalVolume(Length,Width,AvgDepth);
            CalSurfArea(Length, Width);
            CalPoolSize();
            CalHeating();
            
        }

        //This will check if Length of pool is valid.
        public bool ValidLength(float Length)
        {
            bool ValidLength = false;

            //if user input is less then 5 or greater the 50 then this will display the message.
            if (Length >= 5 && Length <= 50)
            {               
            }
            else
            {
                MessageBox.Show("Length measeurement is invalid. Please enter a value between 5 and 50");
            }
            return true;
        }
        
        //This will Check if Width of pool is valid.
        public bool ValidWidth(float Width)
        {
            bool ValidWidth = false;
            // if user input is less then 2 or greater the 20 then this will display the message.
            if (Width >= 2 && Width <= 20)
            {             
            }
            else
            {
                MessageBox.Show("Width measurement is invalid. Please enter a value between 2 and 20");
            }
            return true;
        }

        //This will Check if Average Depth is valid.
        public bool ValidAvgDepth(float AvgDepth)
        {
            bool ValidAvgDepth = false;
            // if user input is less then 2 or greater then 4 then this will display message.
            if (AvgDepth >= 2 && AvgDepth <= 4)
            {                
            }
            else
            {
                MessageBox.Show("Average Depth measurement is invalid. Please enter a value between 2 and 4");
            }
            return true;
        }

        //This will Calculate Surface Area of pool;
        private float CalSurfArea(float Length, float Width)
        {
            txtSurfaceArea.Text = SurfaceArea.ToString();
            
            SurfaceArea = Length * Width;

            return SurfaceArea;     
        }

        //This will Calculate Volume of water in pool.
         private float CalVolume(float Length, float Width, float AvgDepth)
        {
            txtVolume.Text = Volume.ToString();
            
            Volume = Length * Width * AvgDepth * 1000;

            return Volume;
        }

        // This will check the size of the pool to see if the pool is Small, Medium, or Large.
        private double CalPoolSize()
        {
            float PoolSize =0;
            lblCategory.Text = PoolSize.ToString();
             
            if (PoolSize <=SMALLPOOLUPPER)
            {
               lblCategory.Text = "Pool Size: Small";           
            }
            else if (PoolSize >SMALLPOOLUPPER && PoolSize < MEDIUMPOOLUPPER)
            {
                lblCategory.Text = "Pool Size: Medium";
            }
            else if (PoolSize >MEDIUMPOOLUPPER)
            {
                lblCategory.Text = "Pool Size: Large";
            }
            else { }
            return PoolSize;
                     
        }

        //this will calculate the Dollars-per-month of the pool.
        private float CalHeating()
        {
            float heating = AVGTEMPLOWER;
            float step = AVGTEMPSTEP;

            while (heating <= 23)
            {                    
                txtTableTemp.Text += heating.ToString() + "\r\n";                
                txtTableDollars.Text += DollarPerMonth(heating).ToString() + "\r\n";
                heating += step;
            }
            return heating;
        }

        private float DollarPerMonth(float temperature)
        {
          float HeatingCost = 0;
            
            HeatingCost = (TARGETTEMP - temperature) * Volume / HEATINGADJUSTMENT;
            
                return Convert.ToInt32(HeatingCost); 
        }

        // this will make the EXIT button close the application.        
        private void btnExit_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }        
    }
}
