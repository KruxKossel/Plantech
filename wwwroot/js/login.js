function irParaLogin() {
    window.location.href = 'Login.html'
}

window.onload = function(){
    let ir = document.getElementById('ir')
    if (ir) {
        ir.addEventListener('click', irParaLogin)
    }
}


