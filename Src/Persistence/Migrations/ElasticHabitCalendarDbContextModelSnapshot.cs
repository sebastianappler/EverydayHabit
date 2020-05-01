﻿// <auto-generated />
using System;
using ElasticHabitCalendar.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Migrations
{
    [DbContext(typeof(ElasticHabitCalendarDbContext))]
    partial class ElasticHabitCalendarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.HabitCompletion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompletedHabitId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("HabitDifficultyLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompletedHabitId");

                    b.ToTable("HabitCompletions");
                });

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.HabitDifficulty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Definition")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HabitDifficulty");
                });

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.HabitVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EliteId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HabitId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MiniId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EliteId");

                    b.HasIndex("HabitId");

                    b.HasIndex("MiniId");

                    b.HasIndex("PlusId");

                    b.ToTable("HabitVariant");
                });

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.HabitCompletion", b =>
                {
                    b.HasOne("ElasticHabitCalendar.Domain.Entities.Habit", "CompletedHabit")
                        .WithMany()
                        .HasForeignKey("CompletedHabitId");
                });

            modelBuilder.Entity("ElasticHabitCalendar.Domain.Entities.HabitVariant", b =>
                {
                    b.HasOne("ElasticHabitCalendar.Domain.Entities.HabitDifficulty", "Elite")
                        .WithMany()
                        .HasForeignKey("EliteId");

                    b.HasOne("ElasticHabitCalendar.Domain.Entities.Habit", null)
                        .WithMany("Variants")
                        .HasForeignKey("HabitId");

                    b.HasOne("ElasticHabitCalendar.Domain.Entities.HabitDifficulty", "Mini")
                        .WithMany()
                        .HasForeignKey("MiniId");

                    b.HasOne("ElasticHabitCalendar.Domain.Entities.HabitDifficulty", "Plus")
                        .WithMany()
                        .HasForeignKey("PlusId");
                });
#pragma warning restore 612, 618
        }
    }
}
