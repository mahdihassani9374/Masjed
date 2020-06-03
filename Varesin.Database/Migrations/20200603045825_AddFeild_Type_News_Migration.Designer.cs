﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Varesin.Database;

namespace Varesin.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200603045825_AddFeild_Type_News_Migration")]
    partial class AddFeild_Type_News_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Varesin.Database.Identity.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("Gender");

                    b.Property<string>("ImageName");

                    b.Property<bool>("IsSuperAdmin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.ContactUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(200);

                    b.Property<string>("Text")
                        .HasMaxLength(3000);

                    b.HasKey("Id");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("Password");

                    b.Property<int>("Type");

                    b.Property<string>("UserName");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Infoes");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.InstagramTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("InstagramTags");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.LogService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ContentLengthRequest");

                    b.Property<long?>("ContentLengthResponse");

                    b.Property<DateTime>("CreateDate");

                    b.Property<TimeSpan>("Elabsed");

                    b.Property<long?>("ElabsedTime");

                    b.Property<string>("IpAddress");

                    b.Property<string>("Method")
                        .HasMaxLength(100);

                    b.Property<string>("RelativePath")
                        .HasMaxLength(12000);

                    b.Property<int>("ResponseStatusCode");

                    b.Property<string>("UserId")
                        .HasMaxLength(320);

                    b.HasKey("Id");

                    b.ToTable("LogServices");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AttendOnFridays");

                    b.Property<string>("BirthDate");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<string>("EducationalBackground");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("InterviewerDescription");

                    b.Property<string>("InterviewerId")
                        .HasMaxLength(100);

                    b.Property<bool>("IsSingle");

                    b.Property<string>("JihadiHistory");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Skill");

                    b.Property<int>("Status");

                    b.Property<string>("SuggestedFreeTime");

                    b.Property<string>("UniversityName");

                    b.Property<string>("WorkExperienceWithAgeGroup");

                    b.Property<int?>("WorkingGroupId");

                    b.Property<int>("WorkingGroupOfferId");

                    b.HasKey("Id");

                    b.HasIndex("WorkingGroupId");

                    b.HasIndex("WorkingGroupOfferId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<string>("PrimaryPicture")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.NewsFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountDownload");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("Length");

                    b.Property<int>("NewsId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsFiles");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("FullName")
                        .HasMaxLength(300);

                    b.Property<bool>("IsSuccess");

                    b.Property<DateTime?>("PaymentDate");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100);

                    b.Property<long>("Price");

                    b.Property<int?>("RecordId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<string>("PrimaryPicture")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.PostFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountDownload");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("Length");

                    b.Property<int>("PostId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostFiles");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("CostCollected");

                    b.Property<decimal?>("CostEstimation");

                    b.Property<string>("Description");

                    b.Property<string>("Location")
                        .HasMaxLength(128);

                    b.Property<int?>("ReportId");

                    b.Property<int>("State");

                    b.Property<string>("Time")
                        .HasMaxLength(128);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.ProjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("PrimaryPicture")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("ProjectId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("WorkingGroupId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique()
                        .HasFilter("[ProjectId] IS NOT NULL");

                    b.HasIndex("WorkingGroupId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.ReportFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountDownload");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<long>("Length");

                    b.Property<int>("ReportId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportFile");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.SlideShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(128);

                    b.Property<string>("FileName");

                    b.Property<long>("Length");

                    b.Property<string>("Link")
                        .HasMaxLength(128);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("SlideShows");
                });

            modelBuilder.Entity("Varesin.Domain.Entities.WorkingGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("WorkingGroups");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Varesin.Database.Identity.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Varesin.Database.Identity.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Varesin.Database.Identity.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Varesin.Database.Identity.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Member", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.WorkingGroup", "WorkingGroup")
                        .WithMany("Members")
                        .HasForeignKey("WorkingGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Varesin.Domain.Entities.WorkingGroup", "WorkingGroupOffer")
                        .WithMany("MemberOffers")
                        .HasForeignKey("WorkingGroupOfferId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.NewsFile", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.News", "News")
                        .WithMany("Files")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.PostFile", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.Post", "Post")
                        .WithMany("Files")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Project", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.ProjectType", "Type")
                        .WithMany("Projects")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.Report", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.Project", "Project")
                        .WithOne("Report")
                        .HasForeignKey("Varesin.Domain.Entities.Report", "ProjectId");

                    b.HasOne("Varesin.Domain.Entities.WorkingGroup", "WorkingGroup")
                        .WithMany("Reports")
                        .HasForeignKey("WorkingGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Varesin.Domain.Entities.ReportFile", b =>
                {
                    b.HasOne("Varesin.Domain.Entities.Report", "Report")
                        .WithMany("Files")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}