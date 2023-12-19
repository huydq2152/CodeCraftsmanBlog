using Abp.Zero.EntityFrameworkCore;
using CCB.Core.Post;
using Microsoft.EntityFrameworkCore;
using Zero.Authorization.Delegation;
using Zero.Authorization.Roles;
using Zero.Authorization.Users;
using Zero.Chat;
using Zero.Editions;
using Zero.Friendships;
using Zero.MultiTenancy;
using Zero.MultiTenancy.Accounting;
using Zero.MultiTenancy.Payments;
using Zero.Storage;

namespace Zero.EntityFrameworkCore
{
    public class ZeroDbContext : AbpZeroDbContext<Tenant, Role, User, ZeroDbContext>
    {
        #region Zero

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }


        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        #endregion

        #region CCB

        public virtual DbSet<PostCategory> PostCategories { get; set; }

        #endregion

        public ZeroDbContext(DbContextOptions<ZeroDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b => { b.HasIndex(e => new { e.TenantId }); });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });
            
            modelBuilder.Entity<SubscribableEdition>()
                .Property(x => x.AnnualPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SubscribableEdition>()
                .Property(x => x.DailyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SubscribableEdition>()
                .Property(x => x.MonthlyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SubscribableEdition>()
                .Property(x => x.WeeklyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SubscriptionPayment>()
                .Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}