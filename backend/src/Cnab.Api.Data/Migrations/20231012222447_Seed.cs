using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cnab.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO public.users
                (id, login, ""name"", ""password"", created_at, updated_at, removed_at, removed)
                VALUES('5d41275c-80a9-49a4-aa27-2992ccd39a94', 'admin', 'Administrador', '$2a$11$92p9hGGKXtMMDLdlcJF9d.qRob4cZPV5dMJ6xszmbs3sMEuOn.uf6', now(), null, null, false);"
            );

            migrationBuilder.Sql(@"
                INSERT INTO public.transaction_types
                (id, description, kind, signal)
                values
                (1, 'Débito', 'Entrada', '+'),
                (2, 'Boleto', 'Saída', '-'),
                (3, 'Financiamento', 'Saída', '-'),
                (4, 'Crédito', 'Entrada', '+'),
                (5, 'Recebimento Empréstimo', 'Entrada', '+'),
                (6, 'Vendas', 'Entrada', '+'),
                (7, 'Recebimento TED', 'Entrada', '+'),
                (8, 'Recebimento DOC', 'Entrada', '+'),
                (9, 'Aluguel', 'Saída', '-');
            "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete FROM public.users");
            migrationBuilder.Sql(@"delete FROM public.transaction_types");
        }
    }
}
