$(document).ready(function () {
    $('#monthIssuanceFilter, #monthBillingFilter, #monthPaymentFilter, #statusFilter').change(function () {
        applyFilters();
    });

    function applyFilters() {
        var issuanceMonth = $('#monthIssuanceFilter').val();
        var billingMonth = $('#monthBillingFilter').val();
        var paymentMonth = $('#monthPaymentFilter').val();
        var status = $('#statusFilter').val();
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
            },
            error: function () {
                alert('Erro ao carregar dados. Por favor, tente novamente mais tarde.');
            },
        });
    }
});

