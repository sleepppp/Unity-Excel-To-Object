Unity Excel To Object
=======

[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fsleepppp%2FUnity-Excel-To-Object&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)

Read excel file in Unity and create code object

![캡처](https://user-images.githubusercontent.com/32008212/120331760-12b72500-c329-11eb-8357-c55dd0a9b1ba.PNG)

Features
-------------
- Support Office Open XML format (.xlsx)

- I used EPPLUS in load excel;

How to use
-------------

**0. Download Project**

After downloading the compressed file, place the 'ExcelToObject' folder in the Assets folder of your project.

![image](https://user-images.githubusercontent.com/32008212/120430214-a0d6ee00-c3b1-11eb-9651-792c1cb65e6f.png)

**1. Write data to excel**

- The data type must be specified as follows.

- Additional supported data types : array, Vector3, Vector2

![캡처](https://user-images.githubusercontent.com/32008212/120332492-c3bdbf80-c329-11eb-9ea0-3fb7422a30e2.PNG)

**2. Save to assets folder**

![캡처](https://user-images.githubusercontent.com/32008212/120333778-fe742780-c32a-11eb-84c3-9a6aebce304a.PNG)

**3. Create data asset**

- path : Assets/Create/Data/LoadData

![캡처](https://user-images.githubusercontent.com/32008212/120342600-e7393800-c332-11eb-954b-eb284384bccd.PNG)

- After that, you can see that the ScriptableObject is newly created in the Assets folder

![캡처](https://user-images.githubusercontent.com/32008212/120343157-70e90580-c333-11eb-94bd-1af57b433edd.PNG)

**4. Input Creation Info**

- namespace : Generate Code namespace ( can be omitted )

- Folders : Drag and drop folders. Data files and codes are automatically created by reading Excel according to the folder.

![캡처](https://user-images.githubusercontent.com/32008212/120343735-ec4ab700-c333-11eb-9447-b43cfffba6e0.PNG)

**5. Open Create Window**

- path : Window/Data/ExcelData

- Drag and drop the previously created data object into LoadData. And Press Create Button.

- Then it's over

![캡처](https://user-images.githubusercontent.com/32008212/120345275-4a2bce80-c335-11eb-8a9e-15fb5efe66c7.jpg)

**6. It's over**

- Code and TSV file are automatically generated based on Excel.

![image](https://user-images.githubusercontent.com/32008212/120346065-fff71d00-c335-11eb-8d1e-1ef7884add38.png)

![asd](https://user-images.githubusercontent.com/32008212/120346478-5e240000-c336-11eb-8f68-f3ebba7e29b6.PNG)

![캡처](https://user-images.githubusercontent.com/32008212/120346496-611ef080-c336-11eb-8478-ee4d5bbaa94e.PNG)

- In Monobehaviour, you can get data through the GameData class.

![캡처](https://user-images.githubusercontent.com/32008212/120346954-d1c60d00-c336-11eb-9040-27454baef06a.PNG)

