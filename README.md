# AuthorizWorkReservationOder
 Authorization and registration of the user, as well as the selection and reservation of available seats.
 
## Description of the program and tasks for writing this program
The company has shared workplaces.
An employee can choose what he needs for work
(mouse, keyboard, monitor, additional monitor, desktop (if the place is without a desktop, then it comes with his
laptop), etc.). The
employee can enter the date when he wants to work.
An employee sees a list of available jobs and can reserve one for himself.
The administrator can add or remove jobs. He can also change their gear.
Each user will have a specific role, now these are the roles of an employee and an administrator, but the list of roles should be extensible.

Let's move on to the implementation:

## Authorization Panel
The first place where we are transferred from any site is the place of user authorization. Here, the user must enter the correct Username and Password to continue working with the site.

![image](https://user-images.githubusercontent.com/64326994/135439973-4726da6d-a370-42b7-b297-5525a0400842.png)

## Registration Panel
If an ordinary user wants to visit the site, but nothing works out for him, he can use the usual user registration.
When registering, the user must enter only in Latin. After correct input, his data is entered into the database and he goes to the authorization panel

![image](https://user-images.githubusercontent.com/64326994/135440877-43ebb121-6476-493b-bfd6-1456a7148d4a.png)

## User login is not under administrator rights
If the username and password are specified correctly, you will be able to go to the site and see all the functionality.

![Безымянный](https://user-images.githubusercontent.com/64326994/135442086-168c6213-6ead-4920-9e0e-cd0e4a19f1c8.png)

At the sight of all this, you can get a little lost, but in fact everything is very simple here. Let's figure it out.
1. Reservation or reservation status change buttons
    1. The change button is used to change the booking status. Status 1 means that this place is free to order, and status 2 means that the order has been deleted and booked.

     ![image](https://user-images.githubusercontent.com/64326994/135443462-a100a6f7-3935-4301-b3e9-96bcb381f8bb.png)
2. Details Button
    1. This button allows you to see what is in this free space, or rather what devices or equipment is available in this place.
   
    ![image](https://user-images.githubusercontent.com/64326994/135445103-6a8903ac-1371-41ec-8cfb-f1634d2ec4cb.png)

3. About the user
    1. Here the data about the user is displayed, or rather the First and last name under which he entered.
4. Reserved seats
    1. A sign with your reserved seats is displayed here. If you need to delete your reserved seat, you can simply click the Delete button next to the reservation place.
    
    ![image](https://user-images.githubusercontent.com/64326994/135447692-4afbc4c4-46d3-4712-bd9e-7cb65aa0fc25.png)
5. Exit button
    1. When you click this button, you will be able to log out from under this user and return to the authorization panel.
