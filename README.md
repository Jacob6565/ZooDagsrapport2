# Aalborg Zoo Project

The following project was done as part of the third semester of the 2017 fall Software education at Aalborg University.

When receiving the project, Application.StartupUri will be on line 8 in App.xaml.

To open the zookeeper view, in the App.xaml file, replace the Application.StartupUri with:
StartupUri="View/ZooKeeper/MainWindow.xaml" (this will already be the StartupUri when you receive the project)

To open the office view, in the App.xaml file, replace the Application.StartupUri with:
StartupUri="View/OfficeView.xaml" (this will be placed on line 11 when you receive the project)

/**********/
The following line of actions are possible in the program:

* Making and sending an order (for the zookeeper)

1. Open the MainWindow.
2. Navigate to the "foderbestilling" tab.
3. Find the product that you would like to order, and enter an amount and a unit. You can choose multiple.
4. Expand the "medarbejder" tab.
5. Choose an employee.
6. Press the "bestil" button.

* Getting a list of orders to buy (for the shopper)

1. Open the OfficeView.
2. Navigate to the "oversigt" tab.
3. Press the "konverter til PDF" button.
If there are no orders:
4. Make and send some as mentioned in the steps above.
If there are some:
4. Choose where to save the file.
The program will automatically open the newly generated PDF.