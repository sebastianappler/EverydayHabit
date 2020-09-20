﻿// <auto-generated />
using System;
using EverydayHabit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.SQL.Migrations
{
    [DbContext(typeof(EverydayHabitDbContext))]
    [Migration("20200920072032_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EverydayHabit.Domain.Entities.Habit", b =>
                {
                    b.Property<int>("HabitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HabitType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HabitId");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitCompletion", b =>
                {
                    b.Property<int>("HabitCompletionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("HabitDifficultyId")
                        .HasColumnType("int");

                    b.Property<int>("HabitId")
                        .HasColumnType("int");

                    b.Property<int>("HabitVariationId")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<int>("HabitVariationId")
                        .HasColumnType("int");

                    b.HasKey("HabitDifficultyId");

                    b.HasIndex("HabitVariationId");

                    b.ToTable("HabitDifficulties");
                });

            modelBuilder.Entity("EverydayHabit.Domain.Entities.HabitVariation", b =>
                {
                    b.Property<int>("HabitVariationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HabitId")
                        .HasColumnType("int");

                    b.Property<string>("HabitVariantName")
                        .HasColumnType("nvarchar(max)");

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
