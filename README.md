# Pet Store Bia #
---
# Web Services Team "Bia" 
###### Repository link: https://github.com/Telerik-Web-Services-Team-Bia
### Team Members
- Nikolay Novrishki
- Ivalina Popova
- Denica Dimitrova
- Nikolai Mishev
- Ivaylo Andonov

*Pet Store Diagram*
![Draft image for Pet Store](http://oi67.tinypic.com/2d8hpjp.jpg)

## Pet Store Description

The project name is Bia`s Pet Store, which is multi-spa application hosted on Azure Web services, back and front sides both. The main purpose of the app is to help different people to find,shelter or offer the right and sweetiest PET for them. In our store you can see the shared base of pets, choose the best pet for you ,rate the pets, leave offer for selling a pet, ask questions to our support 24/7 and more. All the content in the application is user-generated.

`User Roles`
 * **Not Registered users**
 * * can **view** all pets and offers
 * **Registered users**
 *  * login, logout;
 *  *   can add new pet offer
 *  *   buy pet or delete offer
 *  *  live chat
 *  *  rate different pets


`Categories`
 * dynamic generate categories of pets and filtering them

`Live chat` 
* live messaging between users and online supoort

`Rate`
* logged user can vote for each pet offer

`Send mail`
* sending e-mail to the store

 ## RESTful API Overview
| HTTP Method | Web service endpoint | Description |
|:----------:|:-----------:|:-------------|
|POST (public) | api/Account/Register | Registers a new user in the pet store|
|POST (public) | api/Account/login | Logs in a user in the pet store
|PUT | api/Account/logout | Logs out a user from the events system |
|GET (public)|api/Pets|Gets all pets from the store(sorted or categorized optionaly)|
|POST|api/Pets|Creates a new offer, save it into the MS SQL Database and show it to the client|
|GET|api/Pets/{ID}|Get choosen Pet from the store by given Id|
|DELETE|api/Pets/{ID}|Delete an existing offer by given Id|
|GET(public)|api/Pets?sortBy=[criteria]|Gets the pets sorted by given criteria.|
|GET(public) |api/Pets?Category=[categoryName]|Gets the pets filtered by given category|
|GET|api/Categories|Gets the all categories|
|GET|api/Ratings|Update each pet offer|
|POST|https://mandrillapp.com/api/1.0/messages/send.json|Send mail|

