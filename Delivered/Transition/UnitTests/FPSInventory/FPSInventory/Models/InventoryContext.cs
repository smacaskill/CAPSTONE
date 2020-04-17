using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FPSInventory.Models
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CustomerItem> CustomerItem { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<InItemOrder> InItemOrder { get; set; }
        public virtual DbSet<InOrder> InOrder { get; set; }
        public virtual DbSet<OutItemOrder> OutItemOrder { get; set; }
        public virtual DbSet<OutOrder> OutOrder { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ShippingCompany> ShippingCompany { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=INVENTORY;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Idcategory)
                    .HasName("PK__CATEGORY__32A7B8FA77A6C49A");

                entity.ToTable("CATEGORY");

                entity.Property(e => e.Idcategory).HasColumnName("IDCATEGORY");

                entity.Property(e => e.Namecategory)
                    .IsRequired()
                    .HasColumnName("NAMECATEGORY")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Idcity)
                    .HasName("PK__CITY__0ED2546521209637");

                entity.ToTable("CITY");

                entity.Property(e => e.Idcity).HasColumnName("IDCITY");

                entity.Property(e => e.Namecity)
                    .IsRequired()
                    .HasColumnName("NAMECITY")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("PROVINCE")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerItem>(entity =>
            {
                entity.HasKey(e => e.IdoutItemOrder)
                    .HasName("PK__CUSTOMER__1C7DFB0C016B4505");

                entity.ToTable("CUSTOMER_ITEM");

                entity.Property(e => e.IdoutItemOrder).HasColumnName("IDOUT_ITEM_ORDER");

                entity.Property(e => e.IdCustomerOrder).HasColumnName("ID_CUSTOMER_ORDER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdCustomerOrderNavigation)
                    .WithMany(p => p.CustomerItem)
                    .HasForeignKey(d => d.IdCustomerOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CUSTOMER___ID_CU__5AEE82B9");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.CustomerItem)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CUSTOMER___ID_PR__59FA5E80");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.HasKey(e => e.IdcustomerOrder)
                    .HasName("PK__CUSTOMER__874F8F23B8D0EFDE");

                entity.ToTable("CUSTOMER_ORDER");

                entity.Property(e => e.IdcustomerOrder).HasColumnName("IDCUSTOMER_ORDER");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date");

                entity.Property(e => e.IdStore).HasColumnName("ID_STORE");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.CustomerOrder)
                    .HasForeignKey(d => d.IdStore)
                    .HasConstraintName("FK__CUSTOMER___ID_ST__571DF1D5");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Idemployee)
                    .HasName("PK__EMPLOYEE__B90BBACC9681D6E4");

                entity.ToTable("EMPLOYEE");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__EMPLOYEE__161CF724CD597FB1")
                    .IsUnique();

                entity.Property(e => e.Idemployee).HasColumnName("IDEMPLOYEE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("GENDER")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("ROLE")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InItemOrder>(entity =>
            {
                entity.HasKey(e => e.IdinItemOrder)
                    .HasName("PK__IN_ITEM___895BED0A61F92F45");

                entity.ToTable("IN_ITEM_ORDER");

                entity.Property(e => e.IdinItemOrder).HasColumnName("IDIN_ITEM_ORDER");

                entity.Property(e => e.IdInorder).HasColumnName("ID_INORDER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdInorderNavigation)
                    .WithMany(p => p.InItemOrder)
                    .HasForeignKey(d => d.IdInorder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IN_ITEM_O__ID_IN__534D60F1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.InItemOrder)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IN_ITEM_O__ID_PR__5441852A");
            });

            modelBuilder.Entity<InOrder>(entity =>
            {
                entity.HasKey(e => e.IdinOrder)
                    .HasName("PK__IN_ORDER__6931BE8BC78C5A76");

                entity.ToTable("IN_ORDER");

                entity.Property(e => e.IdinOrder).HasColumnName("IDIN_ORDER");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.Property(e => e.IdShippingCompany).HasColumnName("ID_SHIPPING_COMPANY");

                entity.Property(e => e.IdSupplier).HasColumnName("ID_SUPPLIER");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.InOrder)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IN_ORDER__ID_EMP__46E78A0C");

                entity.HasOne(d => d.IdShippingCompanyNavigation)
                    .WithMany(p => p.InOrder)
                    .HasForeignKey(d => d.IdShippingCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IN_ORDER__ID_SHI__47DBAE45");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.InOrder)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IN_ORDER__ID_SUP__48CFD27E");
            });

            modelBuilder.Entity<OutItemOrder>(entity =>
            {
                entity.HasKey(e => e.IdoutItemOrder)
                    .HasName("PK__OUT_ITEM__1C7DFB0C9453B405");

                entity.ToTable("OUT_ITEM_ORDER");

                entity.Property(e => e.IdoutItemOrder)
                    .HasColumnName("IDOUT_ITEM_ORDER")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdOutorder).HasColumnName("ID_OUTORDER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdOutorderNavigation)
                    .WithMany(p => p.OutItemOrder)
                    .HasForeignKey(d => d.IdOutorder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OUT_ITEM___ID_OU__693CA210");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OutItemOrder)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OUT_ITEM___ID_PR__68487DD7");
            });

            modelBuilder.Entity<OutOrder>(entity =>
            {
                entity.HasKey(e => e.IdoutOrder)
                    .HasName("PK__OUT_ORDE__4673ABA4638C38A1");

                entity.ToTable("OUT_ORDER");

                entity.Property(e => e.IdoutOrder).HasColumnName("IDOUT_ORDER");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.Property(e => e.IdShippingCompany).HasColumnName("ID_SHIPPING_COMPANY");

                entity.Property(e => e.IdStore).HasColumnName("ID_STORE");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.OutOrder)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OUT_ORDER__ID_EM__4BAC3F29");

                entity.HasOne(d => d.IdShippingCompanyNavigation)
                    .WithMany(p => p.OutOrder)
                    .HasForeignKey(d => d.IdShippingCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OUT_ORDER__ID_SH__4CA06362");

                entity.HasOne(d => d.IdStoreNavigation)
                    .WithMany(p => p.OutOrder)
                    .HasForeignKey(d => d.IdStore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OUT_ORDER__ID_ST__4D94879B");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct)
                    .HasName("PK__PRODUCT__ECF6EBFF4A4C300D");

                entity.ToTable("PRODUCT");

                entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");

                entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");

                entity.Property(e => e.Product1)
                    .HasColumnName("PRODUCT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRODUCT__ID_CATE__5070F446");
            });

            modelBuilder.Entity<ShippingCompany>(entity =>
            {
                entity.HasKey(e => e.IdshippingCompany)
                    .HasName("PK__SHIPPING__50394DE0230F1F80");

                entity.ToTable("SHIPPING_COMPANY");

                entity.Property(e => e.IdshippingCompany).HasColumnName("IDSHIPPING_COMPANY");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdCity).HasColumnName("ID_CITY");

                entity.Property(e => e.Namecompany)
                    .HasColumnName("NAMECOMPANY")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasColumnName("PHONENUMBER")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.ShippingCompany)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SHIPPING___ID_CI__3E52440B");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.Idstore)
                    .HasName("PK__STORE__422FCC2C36E01143");

                entity.ToTable("STORE");

                entity.Property(e => e.Idstore).HasColumnName("IDSTORE");

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCity).HasColumnName("ID_CITY");

                entity.Property(e => e.Namestore)
                    .IsRequired()
                    .HasColumnName("NAMESTORE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STORE__ID_CITY__412EB0B6");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Idsupplier)
                    .HasName("PK__SUPPLIER__DD056275CA7BDED7");

                entity.ToTable("SUPPLIER");

                entity.Property(e => e.Idsupplier).HasColumnName("IDSUPPLIER");

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCity).HasColumnName("ID_CITY");

                entity.Property(e => e.Namesupplier)
                    .HasColumnName("NAMESUPPLIER")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SUPPLIER__ID_CIT__440B1D61");
            });
        }
    }
}
