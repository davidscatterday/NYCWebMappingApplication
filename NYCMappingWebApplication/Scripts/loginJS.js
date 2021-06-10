function rbLogIn_Click(myRadio) {
    if (myRadio.value == 1) {
        $('#btnLogIn').show();
        $('#btnSignUp').hide();
    }
    else {
        $('#btnLogIn').hide();
        $('#btnSignUp').show();
    }
}