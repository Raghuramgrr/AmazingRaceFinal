// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var ticker = $.connection.leaderBoardHub, // the generated client-side hub proxy
        $stockTable = $('#stockTable'),
        $stockTableBody = $stockTable.find('tbody'),
        rowTemplate = '<tr data-symbol="{teamName}"><td>{teamName}</td><td>{Position}</td></tr>';
    
    function init() {
        ticker.server.getAllTeams().done(function (stocks) {
            $stockTableBody.empty();
            $.each(stocks, function () {
                var stock = this;
                $stockTableBody.append(rowTemplate.supplant(stock));
            });
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateLeaderBoard = function (stock) {
            $row = $(rowTemplate.supplant(stock));

        $stockTableBody.find('tr[data-symbol='+ stock.teamName +']')
                .replaceWith($row);

    }

    // Start the connection
    $.connection.hub.start().done(init);

});