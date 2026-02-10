const name = document.querySelector(".name")
const mySessionData = sessionStorage.getItem("mySessionStore")
const currentName = JSON.parse(mySessionData)
name.textContent = currentName.userFirstName;
async function   update() {
    const mail = document.querySelector("#userName")
    const userfirstname = document.querySelector("#firstName")
    const userlastname = document.querySelector("#lastName")
    const passwordd = document.querySelector("#password")
    const user = {
        userId: currentName.userId,
        gmail: mail.value,
        userFirstName: userfirstname.value,
        userlastname: userlastname.value,  
            Password: passwordd.value
    }  
    const response = await fetch(`api/users/${user.userId}`,
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body:  JSON.stringify(user)
        });
    
    if (response.ok) { 
        const data = await response.json();
        sessionStorage.setItem("mySessionStore", JSON.stringify(data));
        name.textContent = data.userFirstName;
        alert("העדכון הצליח!");
    }

    else { 
        const errorText = await response.text();
        alert("עדכון נכשל: " + errorText);
    }
}
