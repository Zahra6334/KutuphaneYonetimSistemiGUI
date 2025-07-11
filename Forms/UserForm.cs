using KutuphaneYonetimSistemiGUI.Managers;
using KutuphaneYonetimSistemiGUI.Models;
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
            LoadAvailableBooks();
            LoadBorrowedBooks();
        }

        private void InitializeComponent()
        {
            this.dataGridViewAvailableBooks = new DataGridView();
            this.dataGridViewBorrowedBooks = new DataGridView();
            this.btnRequestLoan = new Button();
            this.btnReturnBook = new Button();

            this.SuspendLayout();

            // 
            // dataGridViewAvailableBooks
            // 
            this.dataGridViewAvailableBooks.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewAvailableBooks.Name = "dataGridViewAvailableBooks";
            this.dataGridViewAvailableBooks.Size = new System.Drawing.Size(400, 150);
            this.dataGridViewAvailableBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAvailableBooks.MultiSelect = false;
            this.dataGridViewAvailableBooks.ReadOnly = true;

            // 
            // dataGridViewBorrowedBooks
            // 
            this.dataGridViewBorrowedBooks.Location = new System.Drawing.Point(12, 200);
            this.dataGridViewBorrowedBooks.Name = "dataGridViewBorrowedBooks";
            this.dataGridViewBorrowedBooks.Size = new System.Drawing.Size(400, 150);
            this.dataGridViewBorrowedBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBorrowedBooks.MultiSelect = false;
            this.dataGridViewBorrowedBooks.ReadOnly = true;

            // 
            // btnRequestLoan
            // 
            this.btnRequestLoan.Location = new System.Drawing.Point(430, 50);
            this.btnRequestLoan.Name = "btnRequestLoan";
            this.btnRequestLoan.Size = new System.Drawing.Size(120, 40);
            this.btnRequestLoan.Text = "Ödünç Al";
            this.btnRequestLoan.Click += new EventHandler(this.btnRequestLoan_Click);

            // 
            // btnReturnBook
            // 
            this.btnReturnBook.Location = new System.Drawing.Point(430, 250);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(120, 40);
            this.btnReturnBook.Text = "İade Et";
            this.btnReturnBook.Click += new EventHandler(this.btnReturnBook_Click);

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
                _loanManager.BorrowBook(_userId, selectedBook.ISBN); // userId int, ISBN string
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
    }
}