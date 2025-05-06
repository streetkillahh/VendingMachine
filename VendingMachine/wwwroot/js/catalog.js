document.addEventListener('DOMContentLoaded', function () {
    const selectedItems = new Set();
    const drinkItems = document.querySelectorAll('.drink-item');
    const brandSelect = document.getElementById('brandSelect');
    const priceRange = document.getElementById('priceRange');
    const priceLabel = priceRange.nextElementSibling;
    const counterBtn = document.getElementById('selectedCountBtn');

    function updateVisibleDrinks() {
        const selectedBrand = brandSelect.value;
        const maxPrice = parseFloat(priceRange.value);

        drinkItems.forEach(item => {
            const brand = item.getAttribute('data-brand');
            const price = parseFloat(item.getAttribute('data-price'));

            const matchBrand = selectedBrand === "" || brand === selectedBrand;
            const matchPrice = price <= maxPrice;

            item.style.display = (matchBrand && matchPrice) ? "block" : "none";
        });

        priceLabel.textContent = `0 руб. - ${maxPrice} руб.`;
    }

    function getSelectedDrinksData() {
        const selected = [];
        document.querySelectorAll('.select-btn[data-selected="true"]').forEach(btn => {
            const id = parseInt(btn.getAttribute('data-id'));
            const card = btn.closest('.card');
            const name = card.querySelector('.card-text').textContent;
            const priceText = card.querySelector('.fw-bold').textContent;
            const price = parseFloat(priceText.replace(/[^\d.]/g, ''));
            const imageUrl = card.querySelector('img').getAttribute('src');

            selected.push({ id, name, price, imageUrl, isAvailable: true, isSelected: true });
        });
        return selected;
    }

    document.getElementById('selectedCountBtn').addEventListener('click', async function (e) {
        e.preventDefault();
        const selectedDrinks = getSelectedDrinksData();

        if (selectedDrinks.length > 0) {
            const selectedIds = selectedDrinks.map(d => d.id);

            try {
                const response = await fetch('/Order/CreateOrder', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(selectedIds)
                });

                if (response.ok) {
                    const data = await response.json();
                    if (data.redirectUrl) {
                        window.location.href = data.redirectUrl;
                    } else {
                        console.error('Не найден redirectUrl в ответе сервера');
                    }
                } else {
                    const errorText = await response.text();
                    alert('Ошибка при создании заказа: ' + errorText);
                }
            } catch (error) {
                console.error('Ошибка при отправке заказа:', error);
                alert('Ошибка при создании заказа.');
            }
        }
    });

    function updatePriceRangeLimits() {
        const selectedBrand = brandSelect.value;
        let prices = [];

        drinkItems.forEach(item => {
            const brand = item.getAttribute('data-brand');
            const price = parseFloat(item.getAttribute('data-price'));

            if (selectedBrand === "" || brand === selectedBrand) {
                prices.push(price);
            }
        });

        if (prices.length > 0) {
            const min = Math.min(...prices);
            const max = Math.max(...prices);
            priceRange.min = min;
            priceRange.max = max;
            priceRange.value = max;
            priceLabel.textContent = `0 руб. - ${max} руб.`;
        }
    }

    brandSelect.addEventListener('change', () => {
        updatePriceRangeLimits();
        updateVisibleDrinks();
    });

    priceRange.addEventListener('input', updateVisibleDrinks);

    // выбор товара
    document.querySelectorAll('.select-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            const id = btn.getAttribute('data-id');
            const selected = btn.getAttribute('data-selected') === 'true';

            if (selected) {
                selectedItems.delete(id);
                btn.classList.remove('btn-success');
                btn.classList.add('btn-warning');
                btn.textContent = 'Выбрать';
                btn.setAttribute('data-selected', 'false');
            } else {
                selectedItems.add(id);
                btn.classList.remove('btn-warning');
                btn.classList.add('btn-success');
                btn.textContent = 'Выбрано';
                btn.setAttribute('data-selected', 'true');
            }

            const count = selectedItems.size;
            counterBtn.textContent = `Выбрано: ${count}`;
            counterBtn.classList.toggle('disabled', count === 0);
        });
    });

    // начальная инициализация
    updatePriceRangeLimits();
    updateVisibleDrinks();
});