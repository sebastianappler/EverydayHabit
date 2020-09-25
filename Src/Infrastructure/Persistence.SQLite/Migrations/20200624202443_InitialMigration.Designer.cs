﻿// <auto-generated />
using System;
using EverydayHabit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EverydayHabit.PersistenceSQLite.Migrations
{
    [DbContext(typeof(EverydayHabitDbContext))]
    [Migration("20200624202443_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("EverydayHabit.Domain.Entities.Habit", b =>
                {
                    b.Property<int>("HabitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("HabitType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("HabitId");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitCompletion", b =>
                {
                    b.Property<int>("HabitCompletionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("HabitDifficultyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HabitId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HabitVariationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HabitCompletionId");

                    b.HasIndex("HabitDifficultyId");

                    b.HasIndex("HabitId");

                    b.HasIndex("HabitVariationId");

                    b.ToTable("HabitCompletions");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitDifficulty", b =>
                {
                    b.Property<int>("HabitDifficultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HabitVariationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HabitDifficultyId");

                    b.HasIndex("HabitVariationId");

                    b.ToTable("HabitDifficulties");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitVariation", b =>
                {
                    b.Property<int>("HabitVariationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HabitId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HabitVariantName")
                        .HasColumnType("TEXT");

                    b.HasKey("HabitVariationId");

                    b.HasIndex("HabitId");

                    b.ToTable("HabitVariations");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitCompletion", b =>
                {
                    b.HasOne("EverydayHabit.Domain.Entities.HabitDifficulty", "HabitDifficulty")
                        .WithMany()
                        .HasForeignKey("HabitDifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EverydayHabit.Domain.Entities.Habit", "Habit")
                        .WithMany()
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EverydayHabit.Domain.Entities.HabitVariation", "HabitVariation")
                        .WithMany()
                        .HasForeignKey("HabitVariationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitDifficulty", b =>
                {
                    b.HasOne("EverydayHabit.Domain.Entities.HabitVariation", "HabitVariation")
                        .WithMany("HabitDifficulties")
                        .HasForeignKey("HabitVariationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitVariation", b =>
                {
                    b.HasOne("EverydayHabit.Domain.Entities.Habit", "Habit")
                        .WithMany("Variants")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}