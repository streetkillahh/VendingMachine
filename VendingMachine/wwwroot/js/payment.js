document.addEventListener('DOMContentLoaded', function () {
    // Инициализация: загружаем сумму заказа из localStorage
    const orderTotal = localStorage.getItem('totalPrice') || 0;
    document.getElementById('orderTotal').textContent = parseFloat(orderTotal).toFixed(0);

    function updateInsertedAmount() {
        let totalInserted = 0;
        document.querySelectorAll('#paymentTable tr').forEach(row => {
            const quantity = parseInt(row.querySelector('.quantity-input').value);
            const value = parseInt(row.getAttribute('data-value'));
            const rowSum = quantity * value;
            row.querySelector('.row-sum').textContent = rowSum;
            totalInserted += rowSum;
        });
        document.getElementById('insertedAmount').textContent = totalInserted.toFixed(0);
        document.getElementById('paidAmountInput').value = totalInserted;

        updatePayButton();
    }

    function updatePayButton() {
        const inserted = parseInt(document.getElementById("insertedAmount").textContent) || 0;
        const required = parseInt(document.getElementById("orderTotal").textContent) || 0;
        document.getElementById("payButton").disabled = inserted < required;
    }

    // Обработчики кнопок увеличения/уменьшения количества монет
    document.querySelectorAll('.increase-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const input = this.closest('.d-flex').querySelector('.quantity-input');
            input.value = parseInt(input.value) + 1;
            updateInsertedAmount();
        });
    });

    document.querySelectorAll('.decrease-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const input = this.closest('.d-flex').querySelector('.quantity-input');
            if (parseInt(input.value) > 0) {
                input.value = parseInt(input.value) - 1;
                updateInsertedAmount();
            }
        });
    });

    // Проверка возможности оплаты
    document.querySelectorAll(".coin-button").forEach(btn => {
        btn.addEventListener("click", updatePayButton);
    });

    // Инициализация
    updateInsertedAmount();
    updatePayButton();
});