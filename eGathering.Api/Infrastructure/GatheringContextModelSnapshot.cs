﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eGathering.Persistence;

#nullable disable

namespace eGathering.Api.Infrastructure
{
    [DbContext(typeof(GatheringContext))]
    partial class GatheringContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Chinese_Taiwan_Stroke_CS_AS")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eGathering.Domain.Attendees.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GatheringId");

                    b.ToTable("Attendee", (string)null);
                });

            modelBuilder.Entity("eGathering.Domain.Gatherings.Gathering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InvitationsExpireUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("MaximumNumberOfAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("NumberOfAttendees")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Gathering", (string)null);
                });

            modelBuilder.Entity("eGathering.Domain.Invitations.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GatheringId");

                    b.ToTable("Invitation", (string)null);
                });

            modelBuilder.Entity("eGathering.Domain.Members.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Member", (string)null);
                });

            modelBuilder.Entity("eGathering.Domain.Attendees.Attendee", b =>
                {
                    b.HasOne("eGathering.Domain.Gatherings.Gathering", "Gathering")
                        .WithMany("Attendees")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gathering");
                });

            modelBuilder.Entity("eGathering.Domain.Gatherings.Gathering", b =>
                {
                    b.HasOne("eGathering.Domain.Members.Member", "Creator")
                        .WithMany("Gatherings")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("eGathering.Domain.Invitations.Invitation", b =>
                {
                    b.HasOne("eGathering.Domain.Gatherings.Gathering", "Gathering")
                        .WithMany("Invitations")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gathering");
                });

            modelBuilder.Entity("eGathering.Domain.Members.Member", b =>
                {
                    b.OwnsOne("eGathering.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .IsUnicode(false)
                                .HasColumnType("varchar(200)")
                                .HasColumnName("Email");

                            b1.HasKey("MemberId");

                            b1.ToTable("Member");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.OwnsOne("eGathering.Domain.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("MemberId");

                            b1.ToTable("Member");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FullName")
                        .IsRequired();
                });

            modelBuilder.Entity("eGathering.Domain.Gatherings.Gathering", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("eGathering.Domain.Members.Member", b =>
                {
                    b.Navigation("Gatherings");
                });
#pragma warning restore 612, 618
        }
    }
}
