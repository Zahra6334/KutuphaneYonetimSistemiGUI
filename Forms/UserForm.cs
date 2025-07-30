using KutuphaneYonetimSistemiGUI.Managers;
using KutuphaneYonetimSistemiGUI.Models;
using KutuphaneYonetimSistemiGUI.SQLManeger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneYonetimSistemiGUI.Forms
{
    public partial class UserForm : Form
    {
        private readonly int _userId;
        private readonly string _userName;
        private readonly BookManager _bookManager;
        private readonly LoanManager _loanManager;
        private readonly SqlUserManager _sqlUserManager;
        // Form kontrolleri
        private DataGridView dataGridViewAvailableBooks;
        private DataGridView dataGridViewBorrowedBooks;
        private Button btnRequestLoan;
        private Button btnReturnBook;

        public UserForm(int userId, string username)
        {
            InitializeComponent();
            _userId = userId;
            _userName = username;
            _bookManager = new BookManager();
            _loanManager = new LoanManager();
            _sqlUserManager = new SqlUserManager();
            LoadAvailableBooks();
            LoadBorrowedBooks();
        }

        private void InitializeComponent()
        {
            this.dataGridViewAvailableBooks = new System.Windows.Forms.DataGridView();
            this.dataGridViewBorrowedBooks = new System.Windows.Forms.DataGridView();
            this.btnRequestLoan = new System.Windows.Forms.Button();
            this.btnReturnBook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailableBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrowedBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAvailableBooks
            // 
            this.dataGridViewAvailableBooks.ColumnHeadersHeight = 29;
            this.dataGridViewAvailableBooks.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewAvailableBooks.MultiSelect = false;
            this.dataGridViewAvailableBooks.Name = "dataGridViewAvailableBooks";
            this.dataGridViewAvailableBooks.ReadOnly = true;
            this.dataGridViewAvailableBooks.RowHeadersWidth = 51;
            this.dataGridViewAvailableBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAvailableBooks.Size = new System.Drawing.Size(400, 150);
            this.dataGridViewAvailableBooks.TabIndex = 0;
            this.dataGridViewAvailableBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAvailableBooks_CellContentClick);
            // 
            // dataGridViewBorrowedBooks
            // 
            this.dataGridViewBorrowedBooks.ColumnHeadersHeight = 29;
            this.dataGridViewBorrowedBooks.Location = new System.Drawing.Point(12, 200);
            this.dataGridViewBorrowedBooks.MultiSelect = false;
            this.dataGridViewBorrowedBooks.Name = "dataGridViewBorrowedBooks";
            this.dataGridViewBorrowedBooks.ReadOnly = true;
            this.dataGridViewBorrowedBooks.RowHeadersWidth = 51;
            this.dataGridViewBorrowedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBorrowedBooks.Size = new System.Drawing.Size(400, 150);
            this.dataGridViewBorrowedBooks.TabIndex = 1;
            this.dataGridViewBorrowedBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBorrowedBooks_CellContentClick);
            // 
            // btnRequestLoan
            // 
            this.btnRequestLoan.Location = new System.Drawing.Point(430, 50);
            this.btnRequestLoan.Name = "btnRequestLoan";
            this.btnRequestLoan.Size = new System.Drawing.Size(120, 40);
            this.btnRequestLoan.TabIndex = 2;
            this.btnRequestLoan.Text = "Ödünç Al";
            this.btnRequestLoan.Click += new System.EventHandler(this.btnRequestLoan_Click);
            // 
            // btnReturnBook
            // 
            this.btnReturnBook.Location = new System.Drawing.Point(430, 250);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(120, 40);
            this.btnReturnBook.TabIndex = 3;
            this.btnReturnBook.Text = "İade Et";
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            // 
            // UserForm
            // 
            this.ClientSize = new System.Drawing.Size(570, 370);
            this.Controls.Add(this.dataGridViewAvailableBooks);
            this.Controls.Add(this.dataGridViewBorrowedBooks);
            this.Controls.Add(this.btnRequestLoan);
            this.Controls.Add(this.btnReturnBook);
            this.Name = "UserForm";
            this.Text = "Kullanıcı Kitap İşlemleri";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailableBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrowedBooks)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoadAvailableBooks()
        {
            dataGridViewAvailableBooks.DataSource = null;
            dataGridViewAvailableBooks.DataSource = _bookManager.GetAllBooks().Where(b => b.IsAvailable).ToList();
        }

        private void LoadBorrowedBooks()
        {
            dataGridViewBorrowedBooks.DataSource = null;
            dataGridViewBorrowedBooks.DataSource = _loanManager.GetBorrowedBooks(_userName);
        }

        private void btnRequestLoan_Click(object sender, EventArgs e)
        {
            if (dataGridViewAvailableBooks.SelectedRows.Count > 0)
            {
                Book selectedBook = (Book)dataGridViewAvailableBooks.SelectedRows[0].DataBoundItem;
                // parametre sırasını ve tipi kontrol et
                _loanManager.BorrowBook(_userId, selectedBook.ISBN);
                
                LoadAvailableBooks();
                LoadBorrowedBooks();
            }
            else
            {
                MessageBox.Show("Lütfen ödünç almak istediğiniz kitabı seçin.");
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (dataGridViewBorrowedBooks.SelectedRows.Count > 0)
            {
                Book selectedBook = (Book)dataGridViewBorrowedBooks.SelectedRows[0].DataBoundItem;
                _loanManager.ReturnBook(selectedBook.ISBN);
                LoadAvailableBooks();
                LoadBorrowedBooks();
            }
            else
            {
                MessageBox.Show("Lütfen iade etmek istediğiniz kitabı seçin.");
            }
        }

        private void dataGridViewAvailableBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewBorrowedBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}