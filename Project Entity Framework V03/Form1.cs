using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_Entity_Framework_V01
{
    public partial class Form1 : Form
    {
        ERP_SystemEntities EF;
        string flag;

        string prod_Name;
        string repo_Name;
        string sup_name;
        DateTime date;
        DateTime EX_date;
        //*--------------------------------------
        //create function to combobox change take params of string---------------------------------------
        //-------------------------------------------------

        public Form1()
        {
            
            InitializeComponent();
            EF = new ERP_SystemEntities();
            flag = string.Empty;
        }

        private void btn_Repositories_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;
            combobox_ID.Items.Clear();
            flag = "REPO";
        
            var query = EF.Repositories.Select(
                r => new { r.ID,r.Name ,r.Manager_Name,r.Factory_ID}).ToList();
            if (query.Count>0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.ID);
                }
                Make_Visible();
            }
        
        }
        private void btn_Products_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;

            combobox_ID.Items.Clear();
            flag = "PROD";

            var query = from i in EF.Products
                        select new { i.ID, i.Code, i.Name, i.Unit_1, i.Unit_2 };
            if (query.Count() > 0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.ID);
                }
                Make_Visible();
            }
        }
        
        private void btn_Supplier_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;

            combobox_ID.Items.Clear();
            flag = "SUPP";

            var query = EF.Suppliers.Select(s => s).ToList();
            dgv_repositories.DataSource = query;
            if (query.Count > 0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.ID);
                }
                Make_Visible();
            }
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;

            combobox_ID.Items.Clear();
            flag = "CUST";

            var query = EF.Customers.Select(c => c).ToList();
            dgv_repositories.DataSource = query;
            if (query.Count > 0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.ID);
                }
                Make_Visible();
            }
        }
        private void btn_SupplyPermission_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;

            combobox_ID.Items.Clear();
            flag = "SUP_PERM";
            var query = EF.Supply_Permissions.Select(c =>
            new {
                c.Permission_Code,
                c.Permission_Date,
                c.Supplier_ID,
                 c.Repository_ID,
                c.Product_ID,
                c.Quantity,
                c.Production_Date,
                c.Expire_Date
            }).ToList();
            dgv_repositories.DataSource = query;
            if (query.Count > 0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.Permission_Code);
                }
                Make_Visible();
            }


        }
        private void btn_SellingPermission_Click(object sender, EventArgs e)
        {
            panel_Repositories.Visible = true;
            panel_Transportation.Visible = false;

            combobox_ID.Items.Clear();
            flag = "SEL_PERM";
            var query = EF.Selling_Permissions.Select(c =>
            new {
                c.Permission_Code,
                c.Permission_Date,
                c.Customer_ID,
                c.Repository_ID,
                c.Product_ID,
                c.Quantity               
            }).ToList();

            dgv_repositories.DataSource = query;
            if (query.Count > 0)
            {
                dgv_repositories.DataSource = query;
                foreach (var item in query)
                {
                    combobox_ID.Items.Add(item.Permission_Code);
                }
                Make_Visible();
            }
        }


        private void dgv_repositories_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is Button)
            {
                Button update_btn = e.Control as Button;
               // update_btn.Click += new EventHandler(update_btn_click);
            }
            
        }


        private void btn_Update_Click(object sender, EventArgs e)
        {
            int id = int.Parse(combobox_ID.SelectedItem.ToString());
            UPDATE_ADD_btn_Click(id);
        }

        private void ShowAll(string flag)
        {
            switch (flag)
            {
                case "REPO":
                   var query1 = EF.Repositories.Select(
                    r => new { r.ID, r.Name, r.Manager_Name, r.Factory_ID }).ToList();
                    dgv_repositories.DataSource = query1;
                    break;

                case "PROD":
                    var query2 = EF.Products.Select(p=> new {p.ID,p.Code,p.Name,p.Unit_1,p.Unit_2 }).ToList();
                    dgv_repositories.DataSource = query2;
                    break;

                case "CUST":
                    var query3 = EF.Customers.Select(c =>new { c.ID, c.Name, c.Phone ,c.Mobile ,c.E_mail,c.Web }).ToList();
                    dgv_repositories.DataSource = query3;
                    break;

                case "SUPP":
                    var query4 = EF.Suppliers.Select(s=> new { s.ID, s.Name, s.Phone, s.Mobile, s.E_mail, s.Web }).ToList();
                    dgv_repositories.DataSource = query4;
                    break;

                case "SUP_PERM":
                    var query5 = EF.Supply_Permissions.Select(c =>
                       new {
                           c.Permission_Code,c.Permission_Date,
                           c.Supplier_ID,c.Repository_ID,
                           c.Product_ID,c.Quantity,
                           c.Production_Date,c.Expire_Date
                       }).ToList();
                     dgv_repositories.DataSource = query5;
                    break;
                case "SEL_PERM":
                    var query6 = EF.Selling_Permissions.Select(c =>
            new {
                c.Permission_Code,c.Permission_Date,
                c.Customer_ID,c.Repository_ID,
                c.Product_ID,c.Quantity
            }).ToList();

                    dgv_repositories.DataSource = query6;
                    break;
                default:
                    break;
            }
        }

        private void Make_Visible()
        {
             combobox_ID.Visible = true;
             btn_Add.Visible = true;
             btn_Update.Visible = true;
        }

        private void combobox_ID_SelectedValueChanged(object sender, EventArgs e)
        {
            btn_Update.Enabled = true;
        }

        private void UPDATE_ADD_btn_Click(int id=0)
        {
           
            if (flag == "REPO")
            {
                Update_Repository repo = new Update_Repository(id);
                repo.ShowDialog();
                ShowAll("REPO");
            }
            else if (flag == "PROD")
            {
                Update_Product prod = new Update_Product(id);
                prod.ShowDialog();
                ShowAll("PROD");
              
            }
            else if (flag == "CUST")
            {
                Update_Customer_Supplier repo = new Update_Customer_Supplier(id, flag);
                repo.ShowDialog();
                ShowAll("CUST");
            }
            else if (flag == "SUPP")
            {
                Update_Customer_Supplier repo = new Update_Customer_Supplier(id, flag);
                repo.ShowDialog();
                ShowAll("SUPP");
            }
            else if (flag == "SEL_PERM")
            {
                Update_Permission per = new Update_Permission(id, flag);
                per.ShowDialog();
                ShowAll("SEL_PERM");

            }
            else if (flag == "SUP_PERM")
            {
                Update_Permission per = new Update_Permission(id, flag);
                per.ShowDialog();
                ShowAll("SUP_PERM");
            }

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
          
            UPDATE_ADD_btn_Click();
        }

        private void btn_Exchange_Click(object sender, EventArgs e)
        {
            panel_Transportation.Visible = true;
            //panel_Repositories.Visible = false;


            comboBoxFrom.Items.Clear();
            comboBoxTo.Items.Clear();
            //comboBoxFrom.SelectedValue = -1;


            var query = EF.Repositories.Select(r =>  r.Name).ToList();
                foreach (var item in query)
                {
                    comboBoxFrom.Items.Add(item);
                    comboBoxTo.Items.Add(item); 
                }

          // comboBoxFrom.SelectedItem = false;

        }

        private void comboBoxFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox_Product.Items.Clear();

            repo_Name = comboBoxFrom.SelectedItem.ToString();
            Repository repo = new Repository();
            var repo1 = EF.Repositories.FirstOrDefault(r => r.Name == repo_Name);
            var prods_id = EF.Repo_Prod.Where(r => r.Repo_ID == repo1.ID).Select(pro => pro.Prod_ID).ToList();

            foreach (var item in prods_id)
            {
                var qq = EF.Products.FirstOrDefault(p => p.ID == item).Name;
                
                comboBox_Product.Items.Add(qq);
            }

        }

        private void comboBox_Product_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox_Supplier.Items.Clear();

             prod_Name = comboBox_Product.SelectedItem.ToString();
             repo_Name = comboBoxFrom.SelectedItem.ToString();

            //need to show supplier name who give this product to that repository

            List<string> supplies = EF.Supply_Permissions.Where(a => a.Product.Name==prod_Name&& a.Repository.Name==repo_Name).Select(a => a.Supplier.Name).ToList();
            foreach (var item in supplies)
            {
                if (!comboBox_Supplier.Items.Contains(item))
                {
                    comboBox_Supplier.Items.Add(item);
                }
            }
        }

        private void comboBox_Supplier_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox_PRo_Date.Items.Clear();

             prod_Name = comboBox_Product.SelectedItem.ToString();
             repo_Name = comboBoxFrom.SelectedItem.ToString();
             sup_name = comboBox_Supplier.SelectedItem.ToString();

            //select production date to prod in that repo With that supplier
            List<DateTime> dates = EF.Supply_Permissions.Where(a =>
            a.Product.Name == prod_Name && a.Repository.Name == repo_Name && a.Supplier.Name==sup_name)
            .Select(d=>d.Production_Date).ToList();

            foreach (var item in dates)
            {
                if (!comboBox_PRo_Date.Items.Contains(item))
                {
                    comboBox_PRo_Date.Items.Add(item);
                }
                
            }

        }

        private void comboBox_PRo_Date_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox_Expire.Items.Clear();

            prod_Name = comboBox_Product.SelectedItem.ToString();
            repo_Name = comboBoxFrom.SelectedItem.ToString();
            sup_name = comboBox_Supplier.SelectedItem.ToString();
            date =DateTime.Parse( comboBox_PRo_Date.SelectedItem.ToString());

            //select production date to prod in that repo With that supplier
           var EX_datesQuery = EF.Supply_Permissions.Where(a =>
            a.Product.Name == prod_Name && a.Repository.Name == repo_Name &&
            a.Supplier.Name == sup_name && a.Production_Date==date)
            .Select(d => d.Expire_Date).ToList();


            foreach (var item in EX_datesQuery)
            {
                if (!comboBox_Expire.Items.Contains(item))
                {
                  comboBox_Expire.Items.Add(item);
                }
            }
        }

        private void comboBox_Expire_SelectedValueChanged(object sender, EventArgs e)
        {
            
             prod_Name = comboBox_Product.SelectedItem.ToString();
             repo_Name = comboBoxFrom.SelectedItem.ToString();
             sup_name = comboBox_Supplier.SelectedItem.ToString();
             date = DateTime.Parse( comboBox_PRo_Date.SelectedItem.ToString());
             EX_date = DateTime.Parse( comboBox_Expire.SelectedItem.ToString());

            //select Quantity of prod in that repo With that supplier and date
            IEnumerable<int> available_Quantity = EF.Supply_Permissions.Where(a =>
            a.Product.Name == prod_Name && a.Repository.Name == repo_Name && a.Supplier.Name == sup_name
             && a.Production_Date == date && a.Expire_Date==EX_date)
            .Select(Q=>Q.Quantity);
            int sum = 0;
            foreach (int item in available_Quantity)
            {
                sum += item;
                
            }
            label7.Text = sum.ToString();
        }

        private void btn_Transport_Click(object sender, EventArgs e)
        {
            // 1..search on repo and product in Repo_Prod  table
            Repo_Prod old_Prod = EF.Repo_Prod.FirstOrDefault(r=>r.Repository.Name==comboBoxFrom.SelectedItem.ToString()
                       &&r.Product.Name==comboBox_Product.Text);
            Repo_Prod new_Prod = EF.Repo_Prod.FirstOrDefault(r => r.Repository.Name == comboBoxTo.SelectedItem.ToString()
                        && r.Product.Name == comboBox_Product.Text);


            // 2..if exist ? change Quantity : else Add new row with new data

            if (old_Prod != null)
            {
                old_Prod.Quantity = old_Prod.Quantity - (int.Parse(textBox1.Text));

            }
            if (new_Prod!=null)
            {
                new_Prod.Quantity = new_Prod.Quantity + (int.Parse(textBox1.Text));

            }
            else if (new_Prod==null)
            {
                Repo_Prod p = new Repo_Prod();
                p.Repo_ID = EF.Repositories.FirstOrDefault(r => r.Name == comboBoxTo.SelectedItem.ToString()).ID;
                p.Prod_ID = EF.Products.FirstOrDefault(r => r.Name == comboBox_Product.SelectedItem.ToString()).ID;

                p.Quantity = int.Parse(textBox1.Text);

                EF.Repo_Prod.AddObject(p);
            }

            EF.SaveChanges();

            MessageBox.Show("Done :)👏 عاش يا وحش");
            panel_Transportation.Visible = false;
     
        }

        private void repositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report_Repository r = new Report_Repository();
            r.ShowDialog();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report_Repository r = new Report_Repository();
            r.ShowDialog();
        }

        private void productMovingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report_Product rp = new Report_Product();
            rp.ShowDialog();
        }
    }
}
