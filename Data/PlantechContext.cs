﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Plantech.Models;

namespace Plantech.Data;

public partial class PlantechContext : DbContext
{
    public PlantechContext()
    {
    }

    public PlantechContext(DbContextOptions<PlantechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargos> Cargos { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Colheitas> Colheitas { get; set; }

    public virtual DbSet<CulturasPerdidas> CulturasPerdidas { get; set; }

    public virtual DbSet<Fornecedores> Fornecedores { get; set; }

    public virtual DbSet<Funcionarios> Funcionarios { get; set; }

    public virtual DbSet<Hortalicas> Hortalicas { get; set; }

    public virtual DbSet<HortalicasPerdidas> HortalicasPerdidas { get; set; }

    public virtual DbSet<HortalicasVendas> HortalicasVendas { get; set; }

    public virtual DbSet<Insumos> Insumos { get; set; }

    public virtual DbSet<InsumosCompras> InsumosCompras { get; set; }

    public virtual DbSet<InsumosPlantios> InsumosPlantios { get; set; }

    public virtual DbSet<LotesHortalicas> LotesHortalicas { get; set; }

    public virtual DbSet<LotesInsumos> LotesInsumos { get; set; }

    public virtual DbSet<OrdensCompras> OrdensCompras { get; set; }

    public virtual DbSet<Plantios> Plantios { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Vendas> Vendas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargos>(entity =>
        {
            entity.ToTable("cargos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.Funcao).HasColumnName("funcao");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.ToTable("clientes");

            entity.HasIndex(e => e.Cnpj, "IX_clientes_cnpj").IsUnique();

            entity.HasIndex(e => e.Cnpj, "idx_clientes_cnpj");

            entity.HasIndex(e => e.RazaoSocial, "idx_clientes_razao_social");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnpj).HasColumnName("cnpj");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Endereco).HasColumnName("endereco");
            entity.Property(e => e.RazaoSocial).HasColumnName("razao_social");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<Colheitas>(entity =>
        {
            entity.ToTable("colheitas");

            entity.HasIndex(e => e.PlantioId, "IX_colheitas_plantio_id").IsUnique();

            entity.HasIndex(e => e.FuncionarioId, "idx_colheitas_funcionario_id");

            entity.HasIndex(e => e.LoteHortalicaId, "idx_colheitas_lote_hortalica_id");

            entity.HasIndex(e => e.LoteInsumoId, "idx_colheitas_lote_insumo_id");

            entity.HasIndex(e => e.PlantioId, "idx_colheitas_plantio_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataColheita).HasColumnName("data_colheita");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.LoteHortalicaId).HasColumnName("lote_hortalica_id");
            entity.Property(e => e.LoteInsumoId).HasColumnName("lote_insumo_id");
            entity.Property(e => e.PlantioId).HasColumnName("plantio_id");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Colheitas)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.LoteHortalica).WithMany(p => p.Colheitas).HasForeignKey(d => d.LoteHortalicaId);

            entity.HasOne(d => d.LoteInsumo).WithMany(p => p.Colheitas).HasForeignKey(d => d.LoteInsumoId);

            entity.HasOne(d => d.Plantio).WithOne(p => p.Colheitas)
                .HasForeignKey<Colheitas>(d => d.PlantioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CulturasPerdidas>(entity =>
        {
            entity.ToTable("culturas_perdidas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ColheitaId).HasColumnName("colheita_id");
            entity.Property(e => e.Nome).HasColumnName("nome");

            entity.HasOne(d => d.Colheita).WithMany(p => p.CulturasPerdidas)
                .HasForeignKey(d => d.ColheitaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Fornecedores>(entity =>
        {
            entity.ToTable("fornecedores");

            entity.HasIndex(e => e.Cnpj, "IX_fornecedores_cnpj").IsUnique();

            entity.HasIndex(e => e.Email, "IX_fornecedores_email").IsUnique();

            entity.HasIndex(e => e.Cnpj, "idx_fornecedores_cnpj");

            entity.HasIndex(e => e.RazaoSocial, "idx_fornecedores_razao_social");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cidade).HasColumnName("cidade");
            entity.Property(e => e.Cnpj).HasColumnName("cnpj");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Endereco).HasColumnName("endereco");
            entity.Property(e => e.RazaoSocial).HasColumnName("razao_social");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Funcionarios>(entity =>
        {
            entity.ToTable("funcionarios");

            entity.HasIndex(e => e.Cpf, "IX_funcionarios_cpf").IsUnique();

            entity.HasIndex(e => e.CargoId, "idx_funcionarios_cargo_id");

            entity.HasIndex(e => e.Cpf, "idx_funcionarios_cpf");

            entity.HasIndex(e => e.UsuarioId, "idx_funcionarios_usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CargoId).HasColumnName("cargo_id");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Funcionarios)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Funcionarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Hortalicas>(entity =>
        {
            entity.ToTable("hortalicas");

            entity.HasIndex(e => e.Nome, "IX_hortalicas_nome").IsUnique();

            entity.HasIndex(e => e.Nome, "idx_hortalicas_nome");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaminhoImagem).HasColumnName("caminho_imagem");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.Observacoes).HasColumnName("observacoes");
            entity.Property(e => e.Status)
                .HasDefaultValue("ativo")
                .HasColumnName("status");
        });

        modelBuilder.Entity<HortalicasPerdidas>(entity =>
        {
            entity.HasKey(e => new { e.CulturaPerdidaId, e.HortalicaId });

            entity.ToTable("hortalicas_perdidas");

            entity.HasIndex(e => e.CulturaPerdidaId, "idx_hortalicas_perdidas_cultura_perdida_id");

            entity.HasIndex(e => e.HortalicaId, "idx_hortalicas_perdidas_hortalica_id");

            entity.Property(e => e.CulturaPerdidaId).HasColumnName("cultura_perdida_id");
            entity.Property(e => e.HortalicaId).HasColumnName("hortalica_id");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.CulturaPerdida).WithMany(p => p.HortalicasPerdidas)
                .HasForeignKey(d => d.CulturaPerdidaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Hortalica).WithMany(p => p.HortalicasPerdidas)
                .HasForeignKey(d => d.HortalicaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<HortalicasVendas>(entity =>
        {
            entity.HasKey(e => new { e.VendaId, e.LoteId });

            entity.ToTable("hortalicas_vendas");

            entity.HasIndex(e => e.LoteId, "idx_hortalicas_vendas_lote_id");

            entity.HasIndex(e => e.VendaId, "idx_hortalicas_vendas_venda_id");

            entity.Property(e => e.VendaId).HasColumnName("venda_id");
            entity.Property(e => e.LoteId).HasColumnName("lote_id");
            entity.Property(e => e.PrecoUnitario).HasColumnName("preco_unitario");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.Lote).WithMany(p => p.HortalicasVendas)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Venda).WithMany(p => p.HortalicasVendas)
                .HasForeignKey(d => d.VendaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Insumos>(entity =>
        {
            entity.ToTable("insumos");

            entity.HasIndex(e => e.FornecedorId, "idx_insumos_fornecedor_id");

            entity.HasIndex(e => e.Nome, "idx_insumos_nome");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaminhoImagem).HasColumnName("caminho_imagem");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.FornecedorId).HasColumnName("fornecedor_id");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.Observacoes).HasColumnName("observacoes");
            entity.Property(e => e.Status)
                .HasDefaultValue("ativo")
                .HasColumnName("status");
            entity.Property(e => e.Tipo).HasColumnName("tipo");

            entity.HasOne(d => d.Fornecedor).WithMany(p => p.Insumos).HasForeignKey(d => d.FornecedorId);
        });

        modelBuilder.Entity<InsumosCompras>(entity =>
        {
            entity.HasKey(e => new { e.OrdemCompraId, e.InsumoId });

            entity.ToTable("insumos_compras");

            entity.HasIndex(e => e.DataChegada, "idx_insumos_compras_data_chegada");

            entity.HasIndex(e => e.LoteId, "idx_insumos_compras_lote_id");

            entity.HasIndex(e => e.OrdemCompraId, "idx_insumos_compras_ordem_compra_id");

            entity.Property(e => e.OrdemCompraId).HasColumnName("ordem_compra_id");
            entity.Property(e => e.InsumoId).HasColumnName("insumo_id");
            entity.Property(e => e.DataChegada).HasColumnName("data_chegada");
            entity.Property(e => e.LoteId).HasColumnName("lote_id");
            entity.Property(e => e.Ponteiro)
                .HasDefaultValueSql("'1'")
                .HasColumnType("INTENGER")
                .HasColumnName("ponteiro");
            entity.Property(e => e.PrecoUnitario).HasColumnName("preco_unitario");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.Insumo).WithMany(p => p.InsumosCompras)
                .HasForeignKey(d => d.InsumoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Lote).WithMany(p => p.InsumosCompras).HasForeignKey(d => d.LoteId);

            entity.HasOne(d => d.OrdemCompra).WithMany(p => p.InsumosCompras)
                .HasForeignKey(d => d.OrdemCompraId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<InsumosPlantios>(entity =>
        {
            entity.HasKey(e => new { e.PlantioId, e.LoteId });

            entity.ToTable("insumos_plantios");

            entity.HasIndex(e => e.LoteId, "idx_insumos_plantios_lote_id");

            entity.HasIndex(e => e.PlantioId, "idx_insumos_plantios_plantio_id");

            entity.Property(e => e.PlantioId).HasColumnName("plantio_id");
            entity.Property(e => e.LoteId).HasColumnName("lote_id");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");

            entity.HasOne(d => d.Lote).WithMany(p => p.InsumosPlantios)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Plantio).WithMany(p => p.InsumosPlantios)
                .HasForeignKey(d => d.PlantioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LotesHortalicas>(entity =>
        {
            entity.ToTable("lotes_hortalicas");

            entity.HasIndex(e => e.HortalicaId, "idx_lotes_hortalicas_hortalica_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataEntrada).HasColumnName("data_entrada");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");
            entity.Property(e => e.HortalicaId).HasColumnName("hortalica_id");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.PrecoVenda).HasColumnName("preco_venda");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Status)
                .HasDefaultValue("ativo")
                .HasColumnName("status");

            entity.HasOne(d => d.Hortalica).WithMany(p => p.LotesHortalicas)
                .HasForeignKey(d => d.HortalicaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LotesInsumos>(entity =>
        {
            entity.ToTable("lotes_insumos");

            entity.HasIndex(e => e.InsumoId, "idx_lotes_insumos_insumo_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataEntrada).HasColumnName("data_entrada");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");
            entity.Property(e => e.InsumoId).HasColumnName("insumo_id");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.PrecoUnitario).HasColumnName("preco_unitario");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Status)
                .HasDefaultValue("ativo")
                .HasColumnName("status");

            entity.HasOne(d => d.Insumo).WithMany(p => p.LotesInsumos)
                .HasForeignKey(d => d.InsumoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrdensCompras>(entity =>
        {
            entity.ToTable("ordens_compras");

            entity.HasIndex(e => e.DataCompra, "idx_ordens_compras_data_compra");

            entity.HasIndex(e => e.FornecedorId, "idx_ordens_compras_fornecedor_id");

            entity.HasIndex(e => e.FuncionarioId, "idx_ordens_compras_funcionario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataCompra).HasColumnName("data_compra");
            entity.Property(e => e.FornecedorId).HasColumnName("fornecedor_id");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.Status)
                .HasDefaultValue("pendente")
                .HasColumnName("status");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Fornecedor).WithMany(p => p.OrdensCompras)
                .HasForeignKey(d => d.FornecedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Funcionario).WithMany(p => p.OrdensCompras)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Plantios>(entity =>
        {
            entity.ToTable("plantios");

            entity.HasIndex(e => e.DataPlantio, "idx_plantios_data");

            entity.HasIndex(e => e.FuncionarioId, "idx_plantios_funcionario_id");

            entity.HasIndex(e => e.HortalicaId, "idx_plantios_hortalica_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataPlantio).HasColumnName("data_plantio");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.HortalicaId).HasColumnName("hortalica_id");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Status)
                .HasDefaultValue("não colhida")
                .HasColumnName("status");

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Plantios)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Hortalica).WithMany(p => p.Plantios)
                .HasForeignKey(d => d.HortalicaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "IX_usuarios_email").IsUnique();

            entity.HasIndex(e => e.NomeUsuario, "IX_usuarios_nome_usuario").IsUnique();

            entity.HasIndex(e => e.Email, "idx_usuarios_email");

            entity.HasIndex(e => e.NomeUsuario, "idx_usuarios_nome_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.NomeUsuario).HasColumnName("nome_usuario");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Senha).HasColumnName("senha");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Vendas>(entity =>
        {
            entity.ToTable("vendas");

            entity.HasIndex(e => e.ClienteId, "idx_vendas_cliente_id");

            entity.HasIndex(e => e.Data, "idx_vendas_data");

            entity.HasIndex(e => e.FuncionarioId, "idx_vendas_funcionario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.FuncionarioId).HasColumnName("funcionario_id");
            entity.Property(e => e.QuantidadeProdutos).HasColumnName("quantidade_produtos");
            entity.Property(e => e.Status)
                .HasDefaultValue("pago")
                .HasColumnName("status");
            entity.Property(e => e.TotalVendas).HasColumnName("total_vendas");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Vendas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Vendas)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
