Filling_Society();

function Filling_User(){
    var u = db.users;

    u.insert({ProfilePicture:'DPzGnqaKrb3zaYi68cjWQ1Qa',FirstName:'TestFirstName1',LastName:'TestLastName1',Email:'test1@gmail.com',Phone:'0606060606',SignupDate:{ $gte: ISODate("2015-09-15T13:20:00.000Z") },Username:'Test1',About:'Oui',Password:'Pass1'})
}

function Filling_Society(){
    var s = db.societies;

    s.insert({SocietyName:'TestSociety1',ManagerId:'DPzGnqaKrb3zaYi68cjWQ1Qa',EmployeeNumber:5,Adress:'36 Rue des coquelicots',Town:'Rennes',Zipcode:'35000',Country:'France',Region:'Bretagne'})
}