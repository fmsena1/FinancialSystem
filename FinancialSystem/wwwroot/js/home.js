$(document).ready(function () {
    var selectedItems = [];
    $.getJSON('/Status/GetStatus', function (data) {
        var filterStatus = $('#statusFilter');
        filterStatus.append('<option value="">Todos</option>');
        console.log(data);
        $.each(data, function (index, value) {
            filterStatus.append('<option value="' + value.value + '">' + value.text + '</option>');
        });

        $('#monthIssuanceFilter, #monthBillingFilter, #monthPaymentFilter, #statusFilter').change(function () {
            applyFilters();
        });

        function applyFilters() {
            var issuanceMonth = $('#monthIssuanceFilter').val();
            var billingMonth = $('#monthBillingFilter').val();
            var paymentMonth = $('#monthPaymentFilter').val();
            var status = $('#statusFilter').val();

            // Mostrar indicador de carregamento
            $('#loadingIndicator').show();

            $.ajax({
                url: '/Home/FilterInvoices',
                type: 'GET',
                data: {
                    issuanceMonth: issuanceMonth,
                    billingMonth: billingMonth,
                    paymentMonth: paymentMonth,
                    status: status
                },
                success: function (data) {
                    $('#invoiceTable tbody').html(data);
                    initializeCheckboxHandlers();
                },
                error: function () {
                    alert('Erro ao carregar dados. Por favor, tente novamente mais tarde.');
                },
                complete: function () {
                    // Esconder indicador de carregamento
                    $('#loadingIndicator').hide();
                }
            });
        }
    });