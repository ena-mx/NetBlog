(function () {
    $('#limitSelect').on('change', function () {
        var val = $(this).val();
        var selector = document.getElementById('limit' + val);
        if (selector) {
            selector.click();
        }
    });
})();