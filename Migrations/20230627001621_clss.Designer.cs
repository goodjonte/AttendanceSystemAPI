﻿// <auto-generated />
using System;
using AttendanceSystemAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceSystemAPI.Migrations
{
    [DbContext(typeof(AttendanceSystemAPIContext))]
    [Migration("20230627001621_clss")]
    partial class clss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttendanceSystemAPI.Models.Attendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassesPeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLate")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("UnjustifiedResolved")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.ClassesPeriods", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ClassesPeriods");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.Enrollments", b =>
                {
                    b.Property<Guid>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EnrollmentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.Notice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NoticeCreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NoticeShowDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoticeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notice");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.School", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("School");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.SchoolClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeachersName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SchoolClass");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.SchoolDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DaysPeriodsJsonArrayString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPeriods")
                        .HasColumnType("int");

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SchoolDay");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.SchoolPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsABreak")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PeriodEndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PeriodNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("PeriodStartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SchoolPeriod");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.SchoolWeek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DailyScheduleJsonArrayString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SameEveryDay")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SchoolWeek");
                });

            modelBuilder.Entity("AttendanceSystemAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CanLogin")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UsersRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
