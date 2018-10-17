# GeopersonServer
MyPersonal Server for my front end apps still on working progress

Goal of this project is to minimize task and time for developing all apps
developers can focus on building the front end with ease
it can be used in different apps depending on the service they needed
it can serve both web/mobile apps

Note:
Services are scattered in the Servers i created


to create database 
change the Web.config Datasource of database
then open your nuget terminal
enable-migrations -contexttype GeopersonContext(Name of the Context) -force
add-migration GeopersonCreate
update-database

that should create the database, tables and its properties


