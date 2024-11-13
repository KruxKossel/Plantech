  // Função para obter os dados e renderizar o gráfico
  async function obterDadosEExibirGrafico() {
    try {
        const response = await fetch('http://localhost:3000/dados-plantios');
        const dados = await response.json();

        // Aqui, adapte os dados para o formato do Chart.js
        const labels = dados.map(dado => dado.nome_do_plantio); // exemplo de label
        const dataValues = dados.map(dado => dado.quantidade); // exemplo de valor

        // Criação do gráfico
        const ctx = document.getElementById('graficoPlantios').getContext('2d');
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Quantidade de Plantios',
                    data: dataValues,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    } catch (error) {
        console.error('Erro ao buscar dados:', error);
    }
}

// Chama a função ao carregar a página
obterDadosEExibirGrafico();