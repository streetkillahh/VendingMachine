document.addEventListener('DOMContentLoaded', function () {
    function updateRowTotal(row) {
        const quantity = parseInt(row.querySelector('.quantity-input').value);
        const unitPrice = parseInt(row.getAttribute('data-price'));
        const total = quantity * unitPrice;
        row.querySelector('.item-total').textContent = total;
    }

    function updateTotalPrice() {
        let total = 0;
        document.querySelectorAll('#orderItems tr').forEach(row => {
            const quantity = parseInt(row.querySelector('.quantity-input').value);
            const unitPrice = parseInt(row.getAttribute('data-price'));
            total += quantity * unitPrice;
        });
        const totalPriceElement = document.getElementById('totalPrice');
        if (totalPriceElement) {
            totalPriceElement.textContent = total;
        }
        localStorage.setItem('totalPrice', total);
    }

    // Обработчики для кнопок увеличения количества
    document.querySelectorAll('.increase-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const row = this.closest('tr');
            const input = row.querySelector('.quantity-input');
            input.value = parseInt(input.value) + 1;
            updateRowTotal(row);
            updateTotalPrice();
        });
    });

    // Обработчики для кнопок уменьшения количества
    document.querySelectorAll('.decrease-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const row = this.closest('tr');
            const input = row.querySelector('.quantity-input');
            if (parseInt(input.value) > 1) {
                input.value = parseInt(input.value) - 1;
                updateRowTotal(row);
                updateTotalPrice();
            }
        });
    });

    // Обработчики для кнопок удаления товара
    document.querySelectorAll('.delete-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const row = this.closest('tr');
            row.remove();
            updateTotalPrice();
        });
    });

    // Инициализация сумм при загрузке страницы
    document.querySelectorAll('#orderItems tr').forEach(row => updateRowTotal(row));
    updateTotalPrice();
});