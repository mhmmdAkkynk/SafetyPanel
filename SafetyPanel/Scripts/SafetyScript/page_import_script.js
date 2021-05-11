//const { ajax } = require("jquery");

const { scriptasync } = require("modernizr");

function passwordControl() {
    var fullName = $("#FullName").val();
    var email = $("#Email").val();
    var password = $("#Password").val();
    var passwordRepeat = $("#Password_Repeat").val();

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: { FullName: fullName, Email: email, Password: password, Password_Repeat: passwordRepeat },
        success: function (postsonucreturn) {
            //var post = JSON.stringify(postsonucreturn)
            if (postsonucreturn[0].Value == "Success") {
                if (postsonucreturn[1].Value == "Ok") {
                    successislemwithmessage("Kaydınız Oluşturuluyor Lütfen Bekleyiniz.", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/await.gif")
                }
                else {
                    hataliislem("")
                }
            }
            else {
                hataliislem("Hata!", postsonucreturn[2].Value);
            }
        }, error: function () {
            hataliislem("Hata!", "Beklenmedik bir hata meydana geldi!");
        }
    })
}

function hataliislem(hatabaslik, hatamesaji, gorsel) {
    if (gorsel != null) {
        Swal.fire({
            //icon: 'error',
            title: '<strong>' + hatabaslik + '</strong>',
            html: hatamesaji,
            showCloseButton: true,
            showCancelButton: true,
            showConfirmButton: false,
            focusCancel: true,
            imageUrl: gorsel,
            cancelButtonText: 'Kapat',
        });
    }
    else {
        Swal.fire({
            icon: 'error',
            title: '<strong>' + hatabaslik + '</strong>',
            html: hatamesaji,
            showCloseButton: true,
            showCancelButton: true,
            showConfirmButton: false,
            focusCancel: true,
            cancelButtonText: 'Kapat',
        });
    }
    
}
function successmessage(successmessage) {
    Swal.fire({
        title: '<strong>Tebrikler!</strong>',
        icon: 'success',
        html: '' + successmessage + '',
        showCloseButton: true,
        showCancelButton: false,
        focusConfirm: true,
        confirmButtonText: 'Geri Dön',
        onClose: function () {
            window.location.reload();
        }
    });
}

function successislemwithmessage(intervalmessage, successmessage, gorsel) {

    $(".modal").modal('hide');
    let timerInterval
    if (gorsel != null) {
        Swal.fire({
            title: '',
            html: '<strong>' + intervalmessage + '</strong>',
            timer: 4000,
            timerProgressBar: true,
            showConfirmButton: false,
            allowOutsideClick: false,
            imageUrl: gorsel,
            onBeforeOpen: () => {
                Swal.showLoading()
                timerInterval = setInterval(() => {
                    const content = Swal.getContent()
                    if (content) {
                        const b = content.querySelector('b')
                        if (b) {
                            //b.textContent = Swal.getTimerLeft()
                        }
                    }
                }, 1000)
            },
            onClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {

                Swal.fire({
                    title: '<strong>Tebrikler!</strong>',
                    icon: 'success',
                    html: '' + successmessage + '',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Giriş Yap',
                    onClose: function () {
                        window.location = "/SignIn/Sign_In";
                    }
                });
            }
        })
    }
    else {
        Swal.fire({
            title: '',
            html: '<strong>' + intervalmessage + '</strong>',
            timer: 4000,
            timerProgressBar: true,
            showConfirmButton: false,
            allowOutsideClick: false,
            onBeforeOpen: () => {
                Swal.showLoading()
                timerInterval = setInterval(() => {
                    const content = Swal.getContent()
                    if (content) {
                        const b = content.querySelector('b')
                        if (b) {
                            //b.textContent = Swal.getTimerLeft()
                        }
                    }
                }, 1000)
            },
            onClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {

                Swal.fire({
                    title: '<strong>Tebrikler!</strong>',
                    icon: 'success',
                    html: '' + successmessage + '',
                    showCloseButton: true,
                    showCancelButton: false,
                    focusConfirm: true,
                    confirmButtonText: 'Giriş Yap',
                    onClose: function () {
                        window.location = "/SignIn/Sign_In";
                    }
                });
            }
        })
    }
    
}

function successResetPasswordwithmessage(intervalmessage, successmessage, gorsel, email) {
    $(".modal").modal('hide');

    let timerInterval
    Swal.fire({
        title: '',
        html: '<strong>' + intervalmessage + '</strong>',
        timer: 4000,
        timerProgressBar: true,
        imageUrl: gorsel,
        showConfirmButton: false,
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                const content = Swal.getContent()
                if (content) {
                    const b = content.querySelector('b')
                    if (b) {
                        //b.textContent = Swal.getTimerLeft()
                    }
                }
            }, 1000)
        },
        onClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {

            Swal.fire({
                title: 'Doğrulama Kodunu Aşağıdaki Alana Giriniz',
                input: 'text',
                inputAttributes: {
                    autocapitalize: 'off',
                    placeholder: "Doğrulama Kodu",
                    id: "Reset_Password",
                    name: "Reset_Password",
                    type: "submit"
                },
                showCancelButton: true,
                confirmButtonText: 'Onayla',
                cancelButtonText: "Çıkış",
                showLoaderOnConfirm: true,
                preConfirm: (login) => {
                    $.ajax({
                        type: "POST",
                        dataType: "JSON",
                        url: "/ResetPassword/CheckResetPassword",
                        data: { Reset_Password_Code: $("#Reset_Password").val() },
                        success: function (postsonucreturn) {
                            if (postsonucreturn[0].Value == "Success") {
                                if (postsonucreturn[1].Value == "Ok") {
                                    window.location = "/ResetPassword/ResetPasswordConfirm?Id=" + postsonucreturn[3].Value + "";
                                }
                                else {
                                    hataliislem("Hata!", postsonucreturn[2].Value);
                                }
                            }
                            else {
                                hataliislem("Hata!", postsonucreturn[2].Value);
                            }

                        }, error: function () {
                            hataliislem("Hata!", "Beklenmedik Bir Hata Gerçekleşti!");
                        }
                    })
                },
                allowOutsideClick: () => !Swal.isLoading()
            });
        }
    })
}

function warningMessage(hatamesaji, gorsel) {
    swal.fire({
        title: "Uyarı!",
        position: "top-end",
        text: hatamesaji,
        imageUrl: gorsel,
        timer: 3500,
        confirmButtonText: "Tamam"
    });
}

function showPassword(value) {
    var password = document.getElementById(value);
    if (password.type === "password") {
        password.type = "text";
    }
    else {
        password.type = "password";
    }
}

function SignIn() {
    var email = $("#EmailSignIn").val();
    var password = $("#PasswordSignIn").val();

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: { Email: email, Password: password },
        success: function (postsonucreturn) {
            if (postsonucreturn[0].Value === "Success") {
                if (postsonucreturn[1].Value === "Ok") {
                    if (postsonucreturn[3].Value === "True") {
                        window.location = "/Home/Home_Page";
                    }
                    else {
                        warningMessage(postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/mail.gif")
                    }
                }
                else {
                    hataliislem("Hata!", postsonucreturn[2].Value)
                }
            }
            else {
                hataliislem("Hata!", postsonucreturn[2].Value)
            }
        }, error: function () {
            hataliislem("Hata!", "Beklenmedik Bir Hata Gerçekleşti!");
        }
    })
}

function ResetPasswordSendEmail() {
    var email = $("#EmailResetPassword").val();

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: { Email: email },
        success: function (postsonucreturn) {
            if (postsonucreturn[0].Value == "Success") {
                if (postsonucreturn[1].Value == "Ok") {
                    successResetPasswordwithmessage("Doğrulama kodunu Eposta olarak gönderiyoruz!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/mail.gif", email);
                }
                else {
                    hataliislem("Hata!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/error.gif");
                }
            }
            else {
                hataliislem("Hata!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/warning.gif");
            }
        }, error: function () {
            hataliislem("Hata!", postsonucreturn[2].Value);
        }
    })
}

function ResetPassword() {
    var id = $("#Id").val();
    var currentPassword = $("#Current_Password").val();
    var newPassword = $("#New_Password").val();
    var newPasswordRepeat = $("#New_Password_Repeat").val();

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: { Id: id, currentPassword: currentPassword, Password: newPassword, Password_Repeat: newPasswordRepeat },
        success: function (postsonucreturn) {
            if (postsonucreturn[0].Value == "Success") {
                if (postsonucreturn[1].Value == "Ok") {
                    successislemwithmessage("Şifreniz Güncelleniyor!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/password.gif");
                }
                else {
                    hataliislem("Hata!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/tenor.gif");
                }
            }
            else {
                hataliislem("Hata!", postsonucreturn[2].Value, "../Template/SafetyPanelTemplate/appstack-bs5.bootlab.io/gif/tenor.gif");
            }
        }, error: function () {
            hataliislem("Hata!", postsonucreturn[2].Value);
        }
    })
}

function AdminPersonelInfo() {
    var firstName = $("#FirstName").val();
    var lastName = $("#LastName").val();
    var email = $("#Email").val();


}

function ChangeProvince() {
    var Id = $("#Province").val();
    alert(Id);
    $("#District").val(Id);
}




