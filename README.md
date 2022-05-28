# RideShare

## ChapterOne

* **Method:**
 `POST`
​/api​/v1​/RideShare​/ChapterOne​/TripRegister
The user should be able to add the travel plan to the system with the information from Where, Where, Date and Explanation, Number of Seats. If there is no User, UserId 0 must be passed

* **Method:**
 `PUT`
**​/api​/v1​/RideShare​/ChapterOne​/TripPublishUpdate**
The user should be able to publish and unpublish the travel plan he/she defines.

* **Method:**
 `GET`
**​/api​/v1​/RideShare​/ChapterOne​/TravelSearch​/{from}​/{to}**
The user should be able to publish and unpublish the travel plan he/she defines.

* **Method:**
 `POST`
**​/api​/v1​/RideShare​/ChapterOne​/TravelJoin**
Users should be able to search for travel plans that are live in the system with From and To information.

## ChapterTwo


* **Method:**
 `POST`
**​/api​/v1​/RideShare​/ChapterTwo​/TripRegister**
The user should be able to add the travel plan to the system with From, To, Date and Description, Number of Seats information

* **Method:**
 `GET`
**​/api​/v1​/RideShare​/ChapterTwo​/FindRoute**
When users search for travel with From and To information, they should be able to find all published travel plans that pass through this route.
