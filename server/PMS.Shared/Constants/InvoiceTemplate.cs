namespace PMS.Shared.Constants;

/// <summary>
/// A static class containing invoice template constants.
/// </summary>
public static class InvoiceTemplate
{
    /// <summary>
    /// The hmtl content of the invoice template.
    /// </summary>
    public const string Content = """
        <!DOCTYPE html>
        <head>
            <meta charset="utf-8">
            <style>
                body {
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 2em 0 0 0;
                }

                .invoice {
                    width: 800px;
                    margin: 0 auto;
                    padding: 20px;
                    border: 1px solid #ccc;
                }

                .header {
                    text-align: center;
                    margin-bottom: 20px;
                }

                .invoice-details {
                    margin-bottom: 20px;
                }

                .invoice-details h2 {
                    margin-bottom: 5px;
                }

                .medicine-list {
                    width: 100%;
                    border-collapse: collapse;
                }

                .medicine-list th,
                .medicine-list td {
                    border: 1px solid #ccc;
                    padding: 8px;
                }

                .total {
                    margin-top: 20px;
                    text-align: right;
                }
            </style>
        </head>
        <body>
            <div class="invoice">
                <div class="header">
                    <h1>Фактура</h1>
                </div>
                <div class="invoice-details">
                    <h2>No: {{InvoiceNumber}}</h2>
                    <h3>Аптека: {{PharmacyName}}</h3>
                    <h3>Адрес: {{Address}}</h3>
                    <h3>Отговорно лице: {{FullName}}</h3>
                </div>
                <table class="medicine-list">
                    <thead>
                        <tr>
                            <th>Лекарство</th>
                            <th>Брой</th>
                            <th>Цена</th>
                        </tr>
                    </thead>
                    <tbody>
                        {{#each Medicines}}
                            <tr>
                                <td>{{this.Name}}</td>
                                <td>{{this.Quantity}}</td>
                                <td>{{this.Price }}</td>
                            </tr>
                        {{/each}}
                    </tbody>
                </table>
                <div class="total">
                    <strong>Крайна цена: ${{TotalPrice}}</strong>
                </div>
                <div>
                    <footer>
                        Дата: {{Date}}
                    </footer>
                </div>
            </div>
        </body>
        </html>
        """;
}