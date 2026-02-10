//function openSignUp() {
//    const newuser = document.querySelector(".newUser")
//    newuser.style.display = "block"
//};
async function signUp() {
    const mail = document.querySelector("#newUserName")
    const userfirstname = document.querySelector("#newFirstName")
    const userlastname = document.querySelector("#newLastName")
    const passwordd = document.querySelector("#newPassword")
    const user = {
        gmail: mail.value,
        userfirstname: userfirstname.value,
        userlastname: userlastname.value,
        password: passwordd.value,
      
    };
          const response = await fetch('api/users',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });

        if (response.status === 201) {
            alert("ברוך הבא");
        }
    
};

async function signIn() {
    const mail = document.querySelector(".userName")
    const passwordd = document.querySelector(".password")
    const user = {
        userId: -15,
        gmail: mail.value,
        userfirstname: "",
        userlastname: "",
        password: passwordd.value
           }

    const response = await fetch('api/users/Login',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
    if (response.status === 200) {
        const responseJson = await response.json();
        sessionStorage.setItem("mySessionStore", JSON.stringify(responseJson))
        alert("משתמש קיים!");
        window.location.href = "home.html";
    }
    else if (response.status === 404) {
        alert("משתמש לא קיים!");
    }
    else {
        alert("שגיאה בשרת!");
    }

};



async function colorDrawing() {
    console.log("login.js loaded");
    const p = document.querySelector("#newPassword");
    const strengthElement = document.querySelector("progress");
    const newPassword = {
        password: p.value,
        Strength: 100
    };
    const response = await checkStrength(newPassword);
    data =await response.json()
    if (response.status === 200) {
         strengthElement.value = data.strength + 1;
    }
    else {
        strengthElement.value = 0;
    }
}


async function checkStrength(newPassword) {
    const response = await fetch('api/password/pass',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newPassword)
        });
    return response;
};