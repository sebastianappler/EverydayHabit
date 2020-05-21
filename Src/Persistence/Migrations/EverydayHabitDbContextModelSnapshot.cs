﻿// <auto-generated />
using System;
using EverydayHabit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Migrations
{
    [DbContext(typeof(EverydayHabitDbContext))]
    partial class EverydayHabitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("EverydayHabit.Domain.Entities.Habit", b =>
                {
                    b.Property<int>("HabitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

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

                    b.Property<int?>("CompletedHabitHabitId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("HabitDifficultyLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("HabitCompletionId");

                    b.HasIndex("CompletedHabitHabitId");

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

                    b.Property<int?>("HabitVariantHabitVariationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HabitDifficultyId");

                    b.HasIndex("HabitVariantHabitVariationId");

                    b.ToTable("HabitDifficulty");
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
                    b.HasOne("EverydayHabit.Domain.Entities.Habit", "CompletedHabit")
                        .WithMany()
                        .HasForeignKey("CompletedHabitHabitId");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitDifficulty", b =>
                {
                    b.HasOne("EverydayHabit.Domain.Entities.HabitVariation", "HabitVariant")
                        .WithMany("HabitDifficulties")
                        .HasForeignKey("HabitVariantHabitVariationId");
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
