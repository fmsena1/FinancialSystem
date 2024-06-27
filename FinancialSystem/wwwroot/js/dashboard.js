$(document).ready(function () {
    var currentYear = new Date().getFullYear();

    $.getJSON('/Year/GetYears', function (data) {
        var filterYear = $('#filterYear');
        filterYear.append('<option value="">Selecione o ano</option>');
        $.each(data, function (index, value) {
            var selected = value.year == currentYear ? ' selected' : '';
            filterYear.append('<option value="' + value.year + '"' + selected + '>' + value.year + '</option>');
        });
    });

    $('#filterYear, #filterMonth, #filterQuarter').change(function () {
        loadData();
    });

    function loadData() {
        var year = $('#filterYear').val() || currentYear;
        var month = $('#filterMonth').val();
        var quarter = $('#filterQuarter').val();

        var period;
        if (month) {
            period = 'month';
        } else if (quarter) {
            period = 'quarter';
        } else {
            period = 'year';
        }

        $.ajax({
            url: '/Dashboard/GetData',
            data: {
                period: period,
                year: year,
                month: month,
                quarter: quarter
            },
            success: function (data) {
                updateChart(mainChart, data.labels, data.values);
                updateChart(overdueChart, data.overdueLabels, data.overdueValues);
                updateChart(revenueChart, data.revenueLabels, data.revenueValues);
            },
            error: function () {
                alert('Erro ao carregar dados.');
            }
        });
    }

    function updateChart(chart, labels, values) {
        chart.data.labels = labels;
        chart.data.datasets[0].data = values;
        chart.update();
    }

    var mainChart = new Chart($('#mainChart'), {
        type: 'bar',
        data: {
            labels: [],
            datasets: [{
                label: 'Valores',
                data: [],
                backgroundColor: 'rgba(0, 123, 255, 0.5)'
            }]
        }
    });

    var overdueChart = new Chart($('#overdueChart'), {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: 'Valores Vencidos',
                data: [],
                backgroundColor: 'rgba(220, 53, 69, 0.5)'
            }]
        }
    });

    var revenueChart = new Chart($('#revenueChart'), {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: 'Valores Recebidos',
                data: [],
                backgroundColor: 'rgba(40, 167, 69, 0.5)'
            }]
        }
    });

    loadData();
});