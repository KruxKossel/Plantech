using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plantech.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alertas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    lote_id = table.Column<int>(type: "INTEGER", nullable: false),
                    tipo = table.Column<string>(type: "TEXT", nullable: false),
                    mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    data_criacao = table.Column<string>(type: "TEXT", nullable: false, defaultValueSql: "date('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alertas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    funcao = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    razao_social = table.Column<string>(type: "TEXT", nullable: false),
                    endereco = table.Column<string>(type: "TEXT", nullable: true),
                    telefone = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    razao_social = table.Column<string>(type: "TEXT", nullable: false),
                    cidade = table.Column<string>(type: "TEXT", nullable: true),
                    endereco = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hortalicas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: true),
                    observacoes = table.Column<string>(type: "TEXT", nullable: true),
                    caminho_imagem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hortalicas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome_usuario = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    senha = table.Column<string>(type: "TEXT", nullable: false),
                    salt = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "insumos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: true),
                    observacoes = table.Column<string>(type: "TEXT", nullable: true),
                    categoria = table.Column<string>(type: "TEXT", nullable: true),
                    tipo = table.Column<string>(type: "TEXT", nullable: false),
                    caminho_imagem = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insumos", x => x.id);
                    table.ForeignKey(
                        name: "FK_insumos_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lotes_hortalicas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    hortalica_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    preco_venda = table.Column<double>(type: "REAL", nullable: true),
                    data_entrada = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    data_validade = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes_hortalicas", x => x.id);
                    table.ForeignKey(
                        name: "FK_lotes_hortalicas_hortalicas_hortalica_id",
                        column: x => x.hortalica_id,
                        principalTable: "hortalicas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cpf = table.Column<string>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    cargo_id = table.Column<int>(type: "INTEGER", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_funcionarios_cargos_cargo_id",
                        column: x => x.cargo_id,
                        principalTable: "cargos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_funcionarios_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lotes_insumos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    insumo_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    preco_unitario = table.Column<double>(type: "REAL", nullable: true),
                    data_entrada = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    data_validade = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes_insumos", x => x.id);
                    table.ForeignKey(
                        name: "FK_lotes_insumos_insumos_insumo_id",
                        column: x => x.insumo_id,
                        principalTable: "insumos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ordens_compras",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    total = table.Column<double>(type: "REAL", nullable: true),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    funcionario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: false),
                    data_compra = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordens_compras", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordens_compras_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ordens_compras_funcionarios_funcionario_id",
                        column: x => x.funcionario_id,
                        principalTable: "funcionarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "plantios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data_plantio = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    hortalica_id = table.Column<int>(type: "INTEGER", nullable: false),
                    funcionario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plantios", x => x.id);
                    table.ForeignKey(
                        name: "FK_plantios_funcionarios_funcionario_id",
                        column: x => x.funcionario_id,
                        principalTable: "funcionarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_plantios_hortalicas_hortalica_id",
                        column: x => x.hortalica_id,
                        principalTable: "hortalicas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vendas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    data = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    total_vendas = table.Column<double>(type: "REAL", nullable: true),
                    quantidade_produtos = table.Column<int>(type: "INTEGER", nullable: true),
                    pagamento = table.Column<string>(type: "TEXT", nullable: true),
                    cliente_id = table.Column<int>(type: "INTEGER", nullable: false),
                    funcionario_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendas", x => x.id);
                    table.ForeignKey(
                        name: "FK_vendas_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vendas_funcionarios_funcionario_id",
                        column: x => x.funcionario_id,
                        principalTable: "funcionarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "insumos_compras",
                columns: table => new
                {
                    ordem_compra_id = table.Column<int>(type: "INTEGER", nullable: false),
                    lote_id = table.Column<int>(type: "INTEGER", nullable: false),
                    insumo_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    preco_unitario = table.Column<double>(type: "REAL", nullable: true),
                    data_chegada = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insumos_compras", x => new { x.ordem_compra_id, x.lote_id });
                    table.ForeignKey(
                        name: "FK_insumos_compras_insumos_insumo_id",
                        column: x => x.insumo_id,
                        principalTable: "insumos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_insumos_compras_lotes_insumos_lote_id",
                        column: x => x.lote_id,
                        principalTable: "lotes_insumos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_insumos_compras_ordens_compras_ordem_compra_id",
                        column: x => x.ordem_compra_id,
                        principalTable: "ordens_compras",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "colheitas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    data_colheita = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    funcionario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    lote_hortalica_id = table.Column<int>(type: "INTEGER", nullable: true),
                    lote_insumo_id = table.Column<int>(type: "INTEGER", nullable: true),
                    plantio_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colheitas", x => x.id);
                    table.ForeignKey(
                        name: "FK_colheitas_funcionarios_funcionario_id",
                        column: x => x.funcionario_id,
                        principalTable: "funcionarios",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_colheitas_lotes_hortalicas_lote_hortalica_id",
                        column: x => x.lote_hortalica_id,
                        principalTable: "lotes_hortalicas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_colheitas_lotes_insumos_lote_insumo_id",
                        column: x => x.lote_insumo_id,
                        principalTable: "lotes_insumos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_colheitas_plantios_plantio_id",
                        column: x => x.plantio_id,
                        principalTable: "plantios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "insumos_plantios",
                columns: table => new
                {
                    plantio_id = table.Column<int>(type: "INTEGER", nullable: false),
                    lote_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insumos_plantios", x => new { x.plantio_id, x.lote_id });
                    table.ForeignKey(
                        name: "FK_insumos_plantios_lotes_insumos_lote_id",
                        column: x => x.lote_id,
                        principalTable: "lotes_insumos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_insumos_plantios_plantios_plantio_id",
                        column: x => x.plantio_id,
                        principalTable: "plantios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "hortalicas_vendas",
                columns: table => new
                {
                    venda_id = table.Column<int>(type: "INTEGER", nullable: false),
                    lote_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    preco_unitario = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hortalicas_vendas", x => new { x.venda_id, x.lote_id });
                    table.ForeignKey(
                        name: "FK_hortalicas_vendas_lotes_hortalicas_lote_id",
                        column: x => x.lote_id,
                        principalTable: "lotes_hortalicas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_hortalicas_vendas_vendas_venda_id",
                        column: x => x.venda_id,
                        principalTable: "vendas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "culturas_perdidas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    colheita_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_culturas_perdidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_culturas_perdidas_colheitas_colheita_id",
                        column: x => x.colheita_id,
                        principalTable: "colheitas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "hortalicas_perdidas",
                columns: table => new
                {
                    cultura_perdida_id = table.Column<int>(type: "INTEGER", nullable: false),
                    hortalica_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hortalicas_perdidas", x => new { x.cultura_perdida_id, x.hortalica_id });
                    table.ForeignKey(
                        name: "FK_hortalicas_perdidas_culturas_perdidas_cultura_perdida_id",
                        column: x => x.cultura_perdida_id,
                        principalTable: "culturas_perdidas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_hortalicas_perdidas_hortalicas_hortalica_id",
                        column: x => x.hortalica_id,
                        principalTable: "hortalicas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_alertas_lote_id_tipo",
                table: "alertas",
                columns: new[] { "lote_id", "tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_clientes_cnpj",
                table: "clientes",
                column: "cnpj");

            migrationBuilder.CreateIndex(
                name: "idx_clientes_razao_social",
                table: "clientes",
                column: "razao_social");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_cnpj",
                table: "clientes",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_colheitas_funcionario_id",
                table: "colheitas",
                column: "funcionario_id");

            migrationBuilder.CreateIndex(
                name: "idx_colheitas_lote_hortalica_id",
                table: "colheitas",
                column: "lote_hortalica_id");

            migrationBuilder.CreateIndex(
                name: "idx_colheitas_lote_insumo_id",
                table: "colheitas",
                column: "lote_insumo_id");

            migrationBuilder.CreateIndex(
                name: "idx_colheitas_plantio_id",
                table: "colheitas",
                column: "plantio_id");

            migrationBuilder.CreateIndex(
                name: "IX_culturas_perdidas_colheita_id",
                table: "culturas_perdidas",
                column: "colheita_id");

            migrationBuilder.CreateIndex(
                name: "idx_fornecedores_cnpj",
                table: "fornecedores",
                column: "cnpj");

            migrationBuilder.CreateIndex(
                name: "idx_fornecedores_razao_social",
                table: "fornecedores",
                column: "razao_social");

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_cnpj",
                table: "fornecedores",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_email",
                table: "fornecedores",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_funcionarios_cargo_id",
                table: "funcionarios",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "idx_funcionarios_cpf",
                table: "funcionarios",
                column: "cpf");

            migrationBuilder.CreateIndex(
                name: "idx_funcionarios_usuario_id",
                table: "funcionarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_cpf",
                table: "funcionarios",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_hortalicas_nome",
                table: "hortalicas",
                column: "nome");

            migrationBuilder.CreateIndex(
                name: "IX_hortalicas_nome",
                table: "hortalicas",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_hortalicas_perdidas_cultura_perdida_id",
                table: "hortalicas_perdidas",
                column: "cultura_perdida_id");

            migrationBuilder.CreateIndex(
                name: "idx_hortalicas_perdidas_hortalica_id",
                table: "hortalicas_perdidas",
                column: "hortalica_id");

            migrationBuilder.CreateIndex(
                name: "idx_hortalicas_vendas_lote_id",
                table: "hortalicas_vendas",
                column: "lote_id");

            migrationBuilder.CreateIndex(
                name: "idx_hortalicas_vendas_venda_id",
                table: "hortalicas_vendas",
                column: "venda_id");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_fornecedor_id",
                table: "insumos",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_nome",
                table: "insumos",
                column: "nome");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_compras_data_chegada",
                table: "insumos_compras",
                column: "data_chegada");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_compras_lote_id",
                table: "insumos_compras",
                column: "lote_id");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_compras_ordem_compra_id",
                table: "insumos_compras",
                column: "ordem_compra_id");

            migrationBuilder.CreateIndex(
                name: "IX_insumos_compras_insumo_id",
                table: "insumos_compras",
                column: "insumo_id");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_plantios_lote_id",
                table: "insumos_plantios",
                column: "lote_id");

            migrationBuilder.CreateIndex(
                name: "idx_insumos_plantios_plantio_id",
                table: "insumos_plantios",
                column: "plantio_id");

            migrationBuilder.CreateIndex(
                name: "idx_lotes_hortalicas_hortalica_id",
                table: "lotes_hortalicas",
                column: "hortalica_id");

            migrationBuilder.CreateIndex(
                name: "idx_lotes_insumos_insumo_id",
                table: "lotes_insumos",
                column: "insumo_id");

            migrationBuilder.CreateIndex(
                name: "idx_ordens_compras_data_compra",
                table: "ordens_compras",
                column: "data_compra");

            migrationBuilder.CreateIndex(
                name: "idx_ordens_compras_fornecedor_id",
                table: "ordens_compras",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "idx_ordens_compras_funcionario_id",
                table: "ordens_compras",
                column: "funcionario_id");

            migrationBuilder.CreateIndex(
                name: "idx_plantios_data",
                table: "plantios",
                column: "data_plantio");

            migrationBuilder.CreateIndex(
                name: "idx_plantios_funcionario_id",
                table: "plantios",
                column: "funcionario_id");

            migrationBuilder.CreateIndex(
                name: "idx_plantios_hortalica_id",
                table: "plantios",
                column: "hortalica_id");

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_email",
                table: "usuarios",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_nome_usuario",
                table: "usuarios",
                column: "nome_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_nome_usuario",
                table: "usuarios",
                column: "nome_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_vendas_cliente_id",
                table: "vendas",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "idx_vendas_data",
                table: "vendas",
                column: "data");

            migrationBuilder.CreateIndex(
                name: "idx_vendas_funcionario_id",
                table: "vendas",
                column: "funcionario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alertas");

            migrationBuilder.DropTable(
                name: "hortalicas_perdidas");

            migrationBuilder.DropTable(
                name: "hortalicas_vendas");

            migrationBuilder.DropTable(
                name: "insumos_compras");

            migrationBuilder.DropTable(
                name: "insumos_plantios");

            migrationBuilder.DropTable(
                name: "culturas_perdidas");

            migrationBuilder.DropTable(
                name: "vendas");

            migrationBuilder.DropTable(
                name: "ordens_compras");

            migrationBuilder.DropTable(
                name: "colheitas");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "lotes_hortalicas");

            migrationBuilder.DropTable(
                name: "lotes_insumos");

            migrationBuilder.DropTable(
                name: "plantios");

            migrationBuilder.DropTable(
                name: "insumos");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "hortalicas");

            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
