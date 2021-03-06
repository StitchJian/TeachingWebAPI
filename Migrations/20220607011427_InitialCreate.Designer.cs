// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using grade_sheet_api.Models;

#nullable disable

namespace grade_sheet_api.Migrations
{
    [DbContext(typeof(GradeContext))]
    [Migration("20220607011427_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("grade_sheet_api.Models.Grade", b =>
                {
                    b.Property<int>("number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("流水號");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("number"), 1L, 1);

                    b.Property<int>("grade")
                        .HasColumnType("int")
                        .HasComment("成績");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("姓名");

                    b.HasKey("number");

                    b.ToTable("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
