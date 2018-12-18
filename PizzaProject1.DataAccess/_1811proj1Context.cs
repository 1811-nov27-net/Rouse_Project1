using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaProject1.DataAccess
{
    public partial class _1811proj1Context : DbContext
    {
        public _1811proj1Context()
        {
        }

        public _1811proj1Context(DbContextOptions<_1811proj1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DatLocationInventory> LocationInventory { get; set; }
        public virtual DbSet<DatLocations> Locations { get; set; }
        public virtual DbSet<DatOrderEntries> OrderEntries { get; set; }
        public virtual DbSet<DatOrders> Orders { get; set; }
        public virtual DbSet<DatPizzaToppings> PizzaToppings { get; set; }
        public virtual DbSet<DatPizzas> Pizzas { get; set; }
        public virtual DbSet<DatToppings> Toppings { get; set; }
        public virtual DbSet<DatUsers> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<DatLocationInventory>(entity =>
            {
                entity.HasKey(e => e.LiId)
                    .HasName("PK_LocationInventory_ID");

                entity.ToTable("LocationInventory", "Piz");

                entity.Property(e => e.LiId).HasColumnName("LI_ID");

                entity.Property(e => e.LiLocation).HasColumnName("LI_Location");

                entity.Property(e => e.LiQuantity).HasColumnName("LI_Quantity");

                entity.Property(e => e.LiTopping).HasColumnName("LI_Topping");

                entity.HasOne(d => d.LiLocationNavigation)
                    .WithMany(p => p.LocationInventory)
                    .HasForeignKey(d => d.LiLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationInventory_Location_LocationID");

                entity.HasOne(d => d.LiToppingNavigation)
                    .WithMany(p => p.LocationInventory)
                    .HasForeignKey(d => d.LiTopping)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationInventory_Topping_ToppingID");
            });

            modelBuilder.Entity<DatLocations>(entity =>
            {
                entity.HasKey(e => e.LId)
                    .HasName("PK_Location_ID");

                entity.ToTable("Locations", "Piz");

                entity.Property(e => e.LId).HasColumnName("L_ID");

                entity.Property(e => e.LCity)
                    .IsRequired()
                    .HasColumnName("L_City")
                    .HasMaxLength(25);

                entity.Property(e => e.LState)
                    .IsRequired()
                    .HasColumnName("L_State")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<DatOrderEntries>(entity =>
            {
                entity.HasKey(e => e.OeId)
                    .HasName("PK_OrderEntry_ID");

                entity.ToTable("OrderEntries", "Piz");

                entity.Property(e => e.OeId).HasColumnName("OE_ID");

                entity.Property(e => e.OeOrder).HasColumnName("OE_Order");

                entity.Property(e => e.OePizza).HasColumnName("OE_Pizza");

                entity.Property(e => e.OeQuantity).HasColumnName("OE_Quantity");

                entity.Property(e => e.OeSubtotal)
                    .HasColumnName("OE_Subtotal")
                    .HasColumnType("money");

                entity.HasOne(d => d.OeOrderNavigation)
                    .WithMany(p => p.OrderEntries)
                    .HasForeignKey(d => d.OeOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderEntry_Order_OrderID");

                entity.HasOne(d => d.OePizzaNavigation)
                    .WithMany(p => p.OrderEntries)
                    .HasForeignKey(d => d.OePizza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderEntry_Pizza_PizzaID");
            });

            modelBuilder.Entity<DatOrders>(entity =>
            {
                entity.HasKey(e => e.OId)
                    .HasName("PK_Order_ID");

                entity.ToTable("Orders", "Piz");

                entity.Property(e => e.OId).HasColumnName("O_ID");

                entity.Property(e => e.OLocation).HasColumnName("O_Location");

                entity.Property(e => e.OTime).HasColumnName("O_Time");

                entity.Property(e => e.OTotalItems).HasColumnName("O_TotalItems");

                entity.Property(e => e.OTotalPrice)
                    .HasColumnName("O_TotalPrice")
                    .HasColumnType("money");

                entity.Property(e => e.OUser).HasColumnName("O_User");

                entity.HasOne(d => d.OLocationNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Location_LocationID");

                entity.HasOne(d => d.OUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User_UserID");
            });

            modelBuilder.Entity<DatPizzaToppings>(entity =>
            {
                entity.HasKey(e => e.PtId)
                    .HasName("PK_PizzaTopping_ID");

                entity.ToTable("PizzaToppings", "Piz");

                entity.Property(e => e.PtId).HasColumnName("PT_ID");

                entity.Property(e => e.PtPizza).HasColumnName("PT_Pizza");

                entity.Property(e => e.PtQuantity).HasColumnName("PT_Quantity");

                entity.Property(e => e.PtTopping).HasColumnName("PT_Topping");

                entity.HasOne(d => d.PtPizzaNavigation)
                    .WithMany(p => p.PizzaToppings)
                    .HasForeignKey(d => d.PtPizza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaTopping_Pizza_PizzaID");

                entity.HasOne(d => d.PtToppingNavigation)
                    .WithMany(p => p.PizzaToppings)
                    .HasForeignKey(d => d.PtTopping)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaTopping_Topping_ToppingID");
            });

            modelBuilder.Entity<DatPizzas>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PK_Pizza_ID");

                entity.ToTable("Pizzas", "Piz");

                entity.Property(e => e.PId).HasColumnName("P_ID");

                entity.Property(e => e.PName)
                    .IsRequired()
                    .HasColumnName("P_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.PPrice)
                    .HasColumnName("P_Price")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<DatToppings>(entity =>
            {
                entity.HasKey(e => e.TId)
                    .HasName("PK_Topping_ID");

                entity.ToTable("Toppings", "Piz");

                entity.Property(e => e.TId).HasColumnName("T_ID");

                entity.Property(e => e.TName)
                    .IsRequired()
                    .HasColumnName("T_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DatUsers>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK_User_ID");

                entity.ToTable("Users", "Piz");

                entity.Property(e => e.UId).HasColumnName("U_ID");

                entity.Property(e => e.UDefaultLocation).HasColumnName("U_DefaultLocation");

                entity.Property(e => e.UFirstName)
                    .IsRequired()
                    .HasColumnName("U_FirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.ULastName)
                    .IsRequired()
                    .HasColumnName("U_LastName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.UDefaultLocationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UDefaultLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Location_LocationID");
            });
        }
    }
}
