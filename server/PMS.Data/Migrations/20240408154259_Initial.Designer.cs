﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMS.Data;

#nullable disable

namespace PMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240408154259_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PMS.Data.Models.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PharmacyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("WorkedHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PharmacyId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PMS.Data.Models.Auth.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Activity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FirstMadeRequest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastMadeRequest")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.BasicMedicine", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BasicMedicines");

                    b.HasData(
                        new
                        {
                            Id = "fd2d444b-066d-459c-bfcb-07abcaa843e6",
                            Description = "Ацетаминофенът се използва за лечение на лека до умерена болка, умерена до силна болка във връзка с опиати или за намаляване на температурата. Често лекуваните състояния включват главоболие, мускулни болки, артрит, болки в гърба, зъбобол, възпалено гърло, настинки, грип и треска.",
                            Name = "Ацетаминофен"
                        },
                        new
                        {
                            Id = "ab3b1230-2ec3-497a-9e36-1bf9768ac33b",
                            Description = "Adderall се използва за лечение на хиперактивно разстройство с дефицит на вниманието (ADHD) и нарколепсия. Adderall съдържа комбинация от амфетамин и декстроамфетамин. Амфетаминът и декстроамфетаминът са стимуланти на централната нервна система, които засягат химикалите в мозъка и нервите, които допринасят за хиперактивност и контрол на импулсите.",
                            Name = "Адерал"
                        },
                        new
                        {
                            Id = "9201e0cd-e454-4b81-a91e-7b34ec52a341",
                            Description = "Амитриптилин е трицикличен антидепресант със седативен ефект. Амитриптилин засяга определени химични посланици (невротрансмитери), които комуникират между мозъчните клетки и помагат за регулиране на настроението.",
                            Name = "Амитриптилин"
                        },
                        new
                        {
                            Id = "c4b4140d-d2eb-4c91-8b49-d5e84c38b0a2",
                            Description = "Амлодипин безилат принадлежи към клас лекарства, наречени блокери на калциевите канали. Понижава кръвното налягане, като отпуска кръвоносните съдове, така че сърцето не трябва да изпомпва толкова силно.",
                            Name = "Амлодипин"
                        },
                        new
                        {
                            Id = "96582e57-4948-4386-a97a-c8e3eae32903",
                            Description = "Ативан (лоразепам) принадлежи към клас лекарства, наречени бензодиазепини. Смята се, че бензодиазепините действат чрез засилване на активността на определени невротрансмитери в мозъка.",
                            Name = "Ативан"
                        },
                        new
                        {
                            Id = "dcd7695f-4c04-45af-8a4f-3afd596c0252",
                            Description = "Аторвастатин принадлежи към клас лекарства, наречени HMG-CoA редуктазни инхибитори (статини). Той действа, като забавя производството на холестерол в тялото, за да намали количеството холестерол, което може да се натрупа по стените на артериите и да блокира притока на кръв към сърцето, мозъка и други части на тялото.",
                            Name = "Аторвастатин"
                        },
                        new
                        {
                            Id = "7af8d779-72d1-432d-a0c5-054f2278e877",
                            Description = "Азитромицинът е антибиотик, който се бори с бактериите. Капсулите Азитромицин АБР се приемат цели, веднъж дневно. Както редица други антибиотици продуктът трябва да се приема най-малко един час преди или два часа след прием на храна.",
                            Name = "Азитромицин"
                        },
                        new
                        {
                            Id = "5dbdfed3-01f9-4c91-968c-c32e3db1f553",
                            Description = "Бензонататът е ненаркотично лекарство за кашлица.",
                            Name = "Бензонатат"
                        },
                        new
                        {
                            Id = "c572ce90-f21b-4e39-8134-217d37710096",
                            Description = "Brilinta предотвратява слепването на тромбоцитите в кръвта и образуването на нежелан кръвен съсирек, който може да блокира артерия.",
                            Name = "Брилинта"
                        },
                        new
                        {
                            Id = "ec93ea37-c0b2-448f-90c9-311c1da3d588",
                            Description = "Букалните филми Bunavail съдържат комбинация от бупренорфин и налоксон. Бупренорфинът е опиоидно лекарство, понякога наричано наркотик. Налоксонът блокира ефектите на опиоидните лекарства, включително облекчаване на болката или усещане за благополучие, което може да доведе до злоупотреба с опиати.",
                            Name = "Bunavail"
                        },
                        new
                        {
                            Id = "8d05a4bb-fe3e-4f47-bc3b-676f6b6d8e16",
                            Description = "Бупренорфинът е опиоидно лекарство, използвано за лечение на разстройство при употреба на опиати (OUD), остра болка и хронична болка.",
                            Name = "Бупренорфин"
                        },
                        new
                        {
                            Id = "5cc29d8f-639f-4ef2-8636-ab39da9f0e16",
                            Description = "Цефалексин е цефалоспоринов (SEF ниско място в) антибиотик. Действа като се бори с бактериите в тялото ви.",
                            Name = "Цефалексин"
                        },
                        new
                        {
                            Id = "9fd753cd-d9dc-4688-b7e3-7359a0a17a71",
                            Description = "Цефалексин е цефалоспо Ципрофлоксацин е флуорохинолонов (flor-o-KWIN-o-lone) антибиотик, използва се за лечение на различни видове бактериални инфекции. Използва се и за лечение на хора, които са били изложени на антракс или определени видове чума. Ципрофлоксацин с удължено освобождаване е одобрен само за употреба при възрастни.rin (SEF антибиотик с ниско съдържание на спор). Действа като се бори с бактериите в тялото ви.",
                            Name = "Ципрофлоксацин"
                        },
                        new
                        {
                            Id = "96bc926e-2c41-4ac6-888d-6da643291756",
                            Description = "Циталопрам е лекарство, отпускано с рецепта, използвано при възрастни за лечение на депресия.",
                            Name = "Циталопрам"
                        },
                        new
                        {
                            Id = "939f1f41-1d91-478e-b1b2-ec461d8a543a",
                            Description = "Клиндамицин е антибиотик, който се бори с бактериите в тялото.",
                            Name = "Клиндамицин"
                        },
                        new
                        {
                            Id = "effec474-6782-4f14-afdf-bb33dc055de3",
                            Description = "Клоназепам е бензодиазепин (ben-zoe-dye-AZE-eh-peen). Смята се, че бензодиазепините действат чрез засилване на активността на определени невротрансмитери в мозъка.",
                            Name = "Клоназепам"
                        },
                        new
                        {
                            Id = "d936ff74-300f-4dfc-8059-ee8fe0259b8e",
                            Description = "Циклобензапринът е мускулен релаксант. Действа, като блокира нервните импулси (или усещанията за болка), които се изпращат до мозъка ви. Циклобензаприн се използва заедно с почивка и физиотерапия за лечение на състояния на скелетните мускули като болка или нараняване.",
                            Name = "Циклобензаприн"
                        },
                        new
                        {
                            Id = "e838e065-bcd0-4d3f-ade5-5b21144348e2",
                            Description = "Cymbalta е селективен антидепресант, инхибитор на обратното захващане на серотонин и норепинефрин (SSNRI). Дулоксетин засяга химикалите в мозъка, които може да са небалансирани при хора с депресия.",
                            Name = "Цимбалта"
                        },
                        new
                        {
                            Id = "26479a38-fa57-4a27-994b-f0b3b29c1249",
                            Description = "Доксициклинът е тетрациклинов антибиотик, който инхибира бактериалния растеж и се смята, че има противовъзпалителни ефекти. Доксициклин се използва за лечение на много различни бактериални инфекции, включително акне, инфекции на пикочните и дихателните пътища, инфекции на очите, заболявания на венците, гонорея, хламидия и сифилис. Може да се използва и за предотвратяване на малария и за лечение на инфекции, причинени от акари, кърлежи или въшки.",
                            Name = "Доксициклин"
                        },
                        new
                        {
                            Id = "520d114a-5dc4-4c98-bf14-7098305c94c3",
                            Description = "Dupixent е инжекционно лекарство с рецепта, използвано за лечение на редица възпалителни състояния.",
                            Name = "Дупиксент"
                        },
                        new
                        {
                            Id = "a450bff9-411c-4abe-89de-39ed9d849eb7",
                            Description = "Entresto съдържа комбинация от сакубитрил и валсартан. Сакубитрил е лекарство за кръвно налягане. Той действа, като повишава нивата на определени протеини в тялото, които могат да разширят (разширят) кръвоносните съдове. Това помага за понижаване на кръвното налягане чрез намаляване на нивата на натрий.",
                            Name = "Ентресто"
                        },
                        new
                        {
                            Id = "d04dbe2e-e0b1-41ec-95fd-b4e737c9f6f0",
                            Description = "Entyvio се използва за лечение на улцерозен колит (UC) или болест на Crohn, която е умерена до тежка при възрастни.",
                            Name = "Entyvio"
                        },
                        new
                        {
                            Id = "19325580-70c7-4362-a0cd-44182770d200",
                            Description = "Farxiga (дапаглифлозин) е перорално лекарство, което може да се дава на определени хора с диабет, сърдечни заболявания или бъбречни заболявания, за да се подобрят техните резултати.",
                            Name = "Фарксига"
                        },
                        new
                        {
                            Id = "fb072c33-6e60-4601-add0-b2b20807ab9a",
                            Description = "Фентанил е синтетичен опиоиден медикамент, използван за лечение на умерена до силна болка, той е до 100 пъти по-силен от други опиоиди като морфин, хероин или оксикодон. Фентанил е от класа лекарства, наречени наркотични аналгетици.",
                            Name = "Фентанил"
                        });
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Depot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Depots");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Medicine", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BasicMedicineId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("PharmacyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BasicMedicineId");

                    b.HasIndex("DepotId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsAssignRequest")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWarning")
                        .HasColumnType("bit");

                    b.Property<string>("PharmacyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("SentOn")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepotId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Pharmacy", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepotId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FounderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DepotId");

                    b.HasIndex("FounderId");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PMS.Data.Models.Auth.ApplicationUser", b =>
                {
                    b.HasOne("PMS.Data.Models.PharmacyEntities.Pharmacy", null)
                        .WithMany("Pharmacists")
                        .HasForeignKey("PharmacyId");
                });

            modelBuilder.Entity("PMS.Data.Models.Auth.RefreshToken", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("PMS.Data.Models.Auth.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Activity", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Depot", b =>
                {
                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.OwnsOne("PMS.Shared.Models.Address", "Address", b1 =>
                        {
                            b1.Property<string>("DepotId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int?>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DepotId");

                            b1.ToTable("Depots");

                            b1.WithOwner()
                                .HasForeignKey("DepotId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Medicine", b =>
                {
                    b.HasOne("PMS.Data.Models.PharmacyEntities.BasicMedicine", "BasicMedicine")
                        .WithMany()
                        .HasForeignKey("BasicMedicineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PMS.Data.Models.PharmacyEntities.Depot", null)
                        .WithMany("Medicines")
                        .HasForeignKey("DepotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PMS.Data.Models.PharmacyEntities.Pharmacy", null)
                        .WithMany("Medicines")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BasicMedicine");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Notification", b =>
                {
                    b.HasOne("PMS.Data.Models.PharmacyEntities.Depot", "Depot")
                        .WithMany()
                        .HasForeignKey("DepotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PMS.Data.Models.PharmacyEntities.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Depot");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Pharmacy", b =>
                {
                    b.HasOne("PMS.Data.Models.PharmacyEntities.Depot", "Depot")
                        .WithMany()
                        .HasForeignKey("DepotId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PMS.Data.Models.Auth.ApplicationUser", "Founder")
                        .WithMany()
                        .HasForeignKey("FounderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PMS.Shared.Models.Address", "Address", b1 =>
                        {
                            b1.Property<string>("PharmacyId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int?>("Number")
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PharmacyId");

                            b1.ToTable("Pharmacies");

                            b1.WithOwner()
                                .HasForeignKey("PharmacyId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Depot");

                    b.Navigation("Founder");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Depot", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("PMS.Data.Models.PharmacyEntities.Pharmacy", b =>
                {
                    b.Navigation("Medicines");

                    b.Navigation("Pharmacists");
                });
#pragma warning restore 612, 618
        }
    }
}
