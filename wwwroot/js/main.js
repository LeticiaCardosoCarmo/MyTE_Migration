document.querySelectorAll('.input-number').forEach(input => {
    input.addEventListener('input', () => {
        calculateTotals();
    });
});

function calculateTotals() {
    const rows = document.querySelectorAll('tbody tr');
    const totals = Array(15).fill(0);
    let grandTotal = 0;

    rows.forEach((row, rowIndex) => {
        if (rowIndex < 4) {
            let rowTotal = 0;
            row.querySelectorAll('.input-number').forEach((input, index) => {
                const value = parseFloat(input.value) || 0;
                rowTotal += value;
                totals[index] += value;
            });
            row.querySelector('.total').value = rowTotal;
            grandTotal += rowTotal;
        }
    });

    const totalRow = rows[4];
    totalRow.querySelectorAll('.total').forEach((input, index) => {
        input.value = totals[index];
    });

    totalRow.querySelector('.grand-total').value = grandTotal;
}

calculateTotals();