function pageLoad() {
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' }).val();

    App.init();
    //UITree.init();
}
