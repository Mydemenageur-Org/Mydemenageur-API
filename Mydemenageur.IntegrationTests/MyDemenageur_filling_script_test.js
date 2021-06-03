function Filling_Client(){
    var c = db.clients;

    c.drop();

    c.insert({_id:ObjectId("93a8ac56bd2d9d2a13995f9b"),UserId:ObjectId("c02da7e40a2ec30b5e60dd89"),Address:"58 Rue de la Pierre",Town:"Rennes",Zipcode:"56000",Country:"France"})
    c.insert({_id:ObjectId("5055d2f398a5efc83f4f19d5"),UserId:ObjectId("da021de5c0eed1ff1668c102"),Address:"24 Rue de la Pierre",Town:"Paris",Zipcode:"95000",Country:"France"})
    c.insert({_id:ObjectId("45d5ae0ad9221e701ceeba5b"),UserId:ObjectId("addc792a46a7f43619201a5b"),Address:"16 Rue de la Pierre",Town:"Lyon",Zipcode:"69000",Country:"France"})
    c.insert({_id:ObjectId("e21cdacaec968c71394e7b10"),UserId:ObjectId("58e36d708a4987491e589c0e"),Address:"42 Rue de la Pierre",Town:"Marseille",Zipcode:"13000",Country:"France"})

}

function Filling_Housing(){
    var h = db.housings;

    h.drop();

    h.insert({_id:ObjectId("9f30c23e20c027855197bfef"),UserId:ObjectId("c02da7e40a2ec30b5e60dd89"),MoveRequestId:ObjectId("412f25549488c88751e09e99"),HousingType:"Maison",HousingFloor:2,IsElevator:true,Surface:25,Address:'36 Rue des coquelicots',Town:'Rennes',Zipcode:'35000',Country:'France',Region:'Bretagne',State:"Start"})
    h.insert({_id:ObjectId("d1b67ce11df5f025d52c36c0"),UserId:ObjectId("da021de5c0eed1ff1668c102"),MoveRequestId:ObjectId("f7970451104bc59785f8b4b4"),HousingType:"Maison",HousingFloor:2,IsElevator:true,Surface:50,Address:'48 Rue des coquelicots',Town:'Paris',Zipcode:'95000',Country:'France',Region:'Ile de France',State:"Start"})
    h.insert({_id:ObjectId("b25a4de53767c286a0a537cf"),UserId:ObjectId("addc792a46a7f43619201a5b"),MoveRequestId:ObjectId("d39ac2a9e907daa5dd1bc23b"),HousingType:"Maison",HousingFloor:1,IsElevator:false,Surface:15,Address:'25 Rue des coquelicots',Town:'Lyon',Zipcode:'69000',Country:'France',Region:'Rhône-Alpes',State:"Start"})
    h.insert({_id:ObjectId("11eec27e7df06675383d1617"),UserId:ObjectId("58e36d708a4987491e589c0e"),MoveRequestId:ObjectId("ee540bb5a8acc64a029b28b7"),HousingType:"Maison",HousingFloor:3,IsElevator:false,Surface:17,Address:'1 Rue des coquelicots',Town:'Marseille',Zipcode:'13000',Country:'France',Region:'Provence-Alpes-Côte d\'Azur',State:"Start"})

    h.insert({_id:ObjectId("74b77d101e82f4563e728baf"),UserId:ObjectId("c02da7e40a2ec30b5e60dd89"),MoveRequestId:ObjectId("412f25549488c88751e09e99"),HousingType:"Maison",HousingFloor:1,IsElevator:true,Surface:25,Address:'36 Rue des coquelicots',Town:'Rennes',Zipcode:'35000',Country:'France',Region:'Bretagne',State:"End"})
    h.insert({_id:ObjectId("e1c729bfffb2c1330814acda"),UserId:ObjectId("da021de5c0eed1ff1668c102"),MoveRequestId:ObjectId("f7970451104bc59785f8b4b4"),HousingType:"Appartement",HousingFloor:10,IsElevator:false,Surface:84,Address:'48 Rue des coquelicots',Town:'Paris',Zipcode:'95000',Country:'France',Region:'Ile de France',State:"End"})
    h.insert({_id:ObjectId("5978170a86bb8de2b2fe3760"),UserId:ObjectId("addc792a46a7f43619201a5b"),MoveRequestId:ObjectId("d39ac2a9e907daa5dd1bc23b"),HousingType:"Maison",HousingFloor:0,IsElevator:false,Surface:13,Address:'25 Rue des coquelicots',Town:'Lyon',Zipcode:'69000',Country:'France',Region:'Rhône-Alpes',State:"End"})
    h.insert({_id:ObjectId("5bea6c406aca43744070f11e"),UserId:ObjectId("58e36d708a4987491e589c0e"),MoveRequestId:ObjectId("ee540bb5a8acc64a029b28b7"),HousingType:"Maison",HousingFloor:2,IsElevator:true,Surface:11,Address:'1 Rue des coquelicots',Town:'Marseille',Zipcode:'13000',Country:'France',Region:'Provence-Alpes-Côte d\'Azur',State:"End"})

}

function Filling_Mover(){
    var m = db.movers;

    m.drop();

    m.insert({ _id: ObjectId("c6b9e1ee60530ec4bc82d701"), UserId: ObjectId("ead29c8d187a26eaf3b39885"), IsVIP:false,AverageCustomerRating:8.5,FileIds:[]})
    m.insert({ _id: ObjectId("8ff13858c921c857cfa53401"), UserId: ObjectId("de2b15c7b3f97f6bdd0d8bde"), IsVIP:true,AverageCustomerRating:9.5,FileIds:[]})
    m.insert({ _id: ObjectId("736216dab52a7dff811ab853"), UserId: ObjectId("6d47538d2d94fea35ad13799"), IsVIP:false,AverageCustomerRating:1.5,FileIds:[]})
    m.insert({ _id: ObjectId("2a14026f67198d33116edd2a"), UserId: ObjectId("bc464241d1887fb7dc1518dc"), IsVIP:false,AverageCustomerRating:5.5,FileIds:[]})
}

function Filling_MoveRequest(){
    var mr = db.moverequests;

    mr.drop();

    mr.insert({_id:ObjectId("412f25549488c88751e09e99"),UserId:ObjectId("c02da7e40a2ec30b5e60dd89"),Title:"Demenagement1",MoveRequestVolume:8,NeedFurnitures:false,NeedAssembly:false,NeedDiassembly:false,MinimumRequestDate:"2015-09-15T13:20:00.000Z",MaximumRequestDate:"2015-09-15T13:20:00.000Z",HeavyFurnitures:["Frigo,Télé"],AdditionalInformation:"Non"})
    mr.insert({ _id: ObjectId("f7970451104bc59785f8b4b4"), UserId: ObjectId("da021de5c0eed1ff1668c102"), Title: "Demenagement2", MoveRequestVolume: 81, NeedFurnitures: true, NeedAssembly: false, NeedDiassembly: false, MinimumRequestDate: "2015-09-15T13:20:00.000Z", MaximumRequestDate: "2015-09-15T13:20:00.000Z",HeavyFurnitures:[],AdditionalInformation:"Non"})
    mr.insert({ _id: ObjectId("d39ac2a9e907daa5dd1bc23b"), UserId: ObjectId("addc792a46a7f43619201a5b"), Title: "Demenagement3", MoveRequestVolume: 28, NeedFurnitures: false, NeedAssembly: true, NeedDiassembly: false, MinimumRequestDate: "2015-09-15T13:20:00.000Z", MaximumRequestDate: "2015-09-15T13:20:00.000Z",HeavyFurnitures:["Frigo"],AdditionalInformation:"Oui surement"})
    mr.insert({ _id: ObjectId("ee540bb5a8acc64a029b28b7"), UserId: ObjectId("58e36d708a4987491e589c0e"), Title: "Demenagement4", MoveRequestVolume: 5, NeedFurnitures: false, NeedAssembly: false, NeedDiassembly: true, MinimumRequestDate: "2015-09-15T13:20:00.000Z", MaximumRequestDate: "2015-09-15T13:20:00.000Z",HeavyFurnitures:["Moto"],AdditionalInformation:"NoUn peun"})

}

function Filling_Society(){
    var s = db.societies;

    s.drop();

    s.insert({_id:ObjectId("507f1f77bcf86cd799439011"),SocietyName:'TestSociety1',ManagerId:ObjectId("ead29c8d187a26eaf3b39885"),EmployeeNumber:5,Address:'36 Rue des coquelicots',Town:'Rennes',Zipcode:'35000',Country:'France',Region:'Bretagne'})
    s.insert({_id:ObjectId("59daf9980effbd5bea0cd89a"),SocietyName:'TestSociety2',ManagerId:ObjectId("de2b15c7b3f97f6bdd0d8bde"),EmployeeNumber:36,Address:'48 Rue des coquelicots',Town:'Paris',Zipcode:'95000',Country:'France',Region:'Ile de France'})
    s.insert({_id:ObjectId("66a02c537bae9ac56aaefe91"),SocietyName:'TestSociety3',ManagerId:ObjectId("6d47538d2d94fea35ad13799"),EmployeeNumber:1,Address:'25 Rue des coquelicots',Town:'Lyon',Zipcode:'69000',Country:'France',Region:'Rhône-Alpes'})
    s.insert({_id:ObjectId("ee98e05f2e7376924d149b2f"),SocietyName:'TestSociety4',ManagerId:ObjectId("bc464241d1887fb7dc1518dc"),EmployeeNumber:2,Address:'1 Rue des coquelicots',Town:'Marseille',Zipcode:'13000',Country:'France',Region:'Provence-Alpes-Côte d\'Azur'})

}

function Filling_User(){
    var u = db.users;

    u.drop();

    u.insert({ _id: ObjectId("c02da7e40a2ec30b5e60dd89"), ProfilePicture: "", FirstName: 'TestFirstName1', LastName: 'TestLastName1', Email: 'test1@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test1', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Client',Token:''})
    u.insert({ _id: ObjectId("da021de5c0eed1ff1668c102"), ProfilePicture: "", FirstName: 'TestFirstName2', LastName: 'TestLastName2', Email: 'test2@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test2', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Client',Token:''})
    u.insert({ _id: ObjectId("addc792a46a7f43619201a5b"), ProfilePicture: "", FirstName: 'TestFirstName3', LastName: 'TestLastName3', Email: 'test3@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test3', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Client',Token:''})
    u.insert({ _id: ObjectId("58e36d708a4987491e589c0e"), ProfilePicture: "", FirstName: 'TestFirstName4', LastName: 'TestLastName4', Email: 'test4@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test4', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Client',Token:''})

    u.insert({ _id: ObjectId("60b6064ff2f2711ff6e96e13"), ProfilePicture: "", FirstName: 'TestFirstName0', LastName: 'TestLastName0', Email: 'test0@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test0', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0, "+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="), Role: 'Client', Token: '' })
    u.insert({ _id: ObjectId("60b751e920d55070861a34a2"), ProfilePicture: "", FirstName: 'TestFirstName01', LastName: 'TestLastName01', Email: 'test01@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test01', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0, "+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="), Role: 'Mover', Token: '' })

    u.insert({ _id: ObjectId("ead29c8d187a26eaf3b39885"), ProfilePicture: "", FirstName: 'TestFirstName5', LastName: 'TestLastName5', Email: 'test5@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test5', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Mover',Token:''})
    u.insert({ _id: ObjectId("de2b15c7b3f97f6bdd0d8bde"), ProfilePicture: "", FirstName: 'TestFirstName6', LastName: 'TestLastName6', Email: 'test6@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test6', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Mover',Token:''})
    u.insert({ _id: ObjectId("6d47538d2d94fea35ad13799"), ProfilePicture: "", FirstName: 'TestFirstName7', LastName: 'TestLastName7', Email: 'test7@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test7', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Mover',Token:''})
    u.insert({ _id: ObjectId("bc464241d1887fb7dc1518dc"), ProfilePicture: "", FirstName: 'TestFirstName8', LastName: 'TestLastName8', Email: 'test8@gmail.com', Phone: '0606060606', SignupDate: "2015-09-15T13:20:00.000Z", Username: 'Test8', About: 'Oui', PasswordHash: 'jJqxkrDYatgjwJ0OuIMsyjj/fQhNuufwdxl6fdXi0RbmixdUCiX6ZBZMh2AGLdkzIWKQ10hl8+T8XSRIKxj/nA==', PasswordSalt: BinData(0,"+LvKyoiGHJ+OqjPl1qO/VO1zvyOblQTXBXcTWvEgrG3HjMBxIK6FJUd39J+grMsNA9DpHsBlZGOigaa8mFlQ1M8AsCuWW8D3gy3T7Ztk8zvpOEFMn5Q5papHRhDf5paGfDg+327j9i0OlxClpuFVKK1+8OFEafHggFehtFDwtdo="),Role:'Mover',Token:''})

}

function Filling_Vehicles(){
    var v = db.Vehicles;

    v.drop();

    v.insert({_id:ObjectId("728387adfe9be99566803885"),SocietyId:ObjectId("507f1f77bcf86cd799439011"),VehiclesNumber:2,HasTarpaulinVehicule:true,PTAC_TarpaulinVehicule:1500,HasHardWallVehicule:true,PTAC_HardWallVehicule:2500,CanTransportHorse:true,CanTransportHorse:true,TotalCapacity:4000})
    v.insert({_id:ObjectId("a53cf6867de07075adfdb82e"),SocietyId:ObjectId("59daf9980effbd5bea0cd89a"),VehiclesNumber:1,HasTarpaulinVehicule:true,PTAC_TarpaulinVehicule:1700,HasHardWallVehicule:false,PTAC_HardWallVehicule:0,CanTransportHorse:false,CanTransportHorse:false,TotalCapacity:1700})
    v.insert({_id:ObjectId("25f5db764122886ad306169d"),SocietyId:ObjectId("66a02c537bae9ac56aaefe91"),VehiclesNumber:4,HasTarpaulinVehicule:true,PTAC_TarpaulinVehicule:1500,HasHardWallVehicule:true,PTAC_HardWallVehicule:2500,CanTransportHorse:true,CanTransportHorse:true,TotalCapacity:8000})
    v.insert({_id:ObjectId("dfce265168fe9d08eb790057"),SocietyId:ObjectId("ee98e05f2e7376924d149b2f"),VehiclesNumber:3,HasTarpaulinVehicule:true,PTAC_TarpaulinVehicule:1500,HasHardWallVehicule:true,PTAC_HardWallVehicule:2500,CanTransportHorse:true,CanTransportHorse:true,TotalCapacity:5500})
}

Filling_Client();
Filling_Housing();
Filling_Mover();
Filling_MoveRequest();
Filling_Society();
Filling_User();
Filling_Vehicles();
