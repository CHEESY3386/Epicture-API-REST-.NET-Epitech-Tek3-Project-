import 'dart:convert';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: App(),
    );
  }
}

class App extends StatefulWidget {
  @override
  AppState createState() => AppState();
}

class AppState extends State<App> {
  static List<Widget> _allPages = <Widget>[
    AccountPage(),
    ImagePage(),
    FavoritePage(),
  ];

  int _page = 0;

  void changePage(int index) {
    setState(() {
      _page = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("fodid"),
      ),
      body: _allPages.elementAt(_page),
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: Colors.blue,
        selectedItemColor: Colors.white,
        items: <BottomNavigationBarItem>[
          BottomNavigationBarItem(
              icon: Icon(Icons.home), title: Text("Account")),
          BottomNavigationBarItem(
              icon: Icon(Icons.image), title: Text("Images")),
          BottomNavigationBarItem(
              icon: Icon(Icons.stars), title: Text("Favorites")),
        ],
        currentIndex: _page,
        onTap: changePage,
      ),
    );
  }
}

class DisplayImage extends StatelessWidget {
  final String imageurl;
  DisplayImage({Key key, @required this.imageurl}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
        child: GestureDetector(
            child: Image(
              image: NetworkImage(imageurl),
            ),
            onTap: () {
              Navigator.pop(context, true);
            }));
  }
}

class AccountPage extends StatefulWidget {
  @override
  AccountPageState createState() => AccountPageState();
}

// class ImagesInfos {
//   final String title;
//   final String url;

//   ImagesInfos({this.title, this.url});

//   factory ImagesInfos.fromJson(Map<String, dynamic> json) {
//     return ImagesInfos(
//       title: json['data/title'],
//       url: json['data/images/link']
//     );
//   }
// }

//Future<ImagesInfos> fetchImages() async {
//'https://localhost:5001/image/viral/year/1/search?q=cat';

// String url = 'https://10.10.251.132:5001/image/viral/year/1/search?q=cat';

// Map<String, String> _headers = {
//   'Authorization': 'bearer 59879dfa9748ee7ae6a68693b0fcc1598cde36fe',
//   'Content-Type': 'application/json',
// };
//   final response = await http.get(url, headers: _headers);

//   if (response.statusCode == 200) {
//     return ImagesInfos.fromJson(json.decode(response.body));
//   } else {
//     throw Exception('Failed to load post');
//   }
// }

class AccountPageState extends State<AccountPage> {
// Future<ImagesInfos> imagesInfos;

  // @override
  // void initState() {
  //   super.initState();
  //   imagesInfos = fetchImages();
  // }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: Center(child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Icon(Icons.person, size: 200),
            Text("aureliendlc99@gmail.com\n"),
            Text("test mdp"),
        //      FutureBuilder<ImagesInfos>(
        // future: imagesInfos,
        // builder: (context, snapshot) {
        //   if (snapshot.hasData) {
        //     return Text(snapshot.data.title);
        //   } else if (snapshot.hasError) {
        //     return Text("${snapshot.error}");
        //   }

        //   // By default, show a loading spinner.
        //   return CircularProgressIndicator();
        // })
          ],),),
      
    );
  }
}

class FavoritePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body:
      Center(child: Text("ici mes favoris"),)
    );
  }
}

class UploadPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body:
      Center(child: Text("ici mes Upload"),)
    );
  }
}

class ImagePage extends StatefulWidget {
  @override
  ImagePageState createState() => ImagePageState();
}

class ImagePageState extends State<ImagePage> {
 List<String> _allImages = [
    "https://media.senscritique.com/media/000015747945/source_big/My_Hero_Academia.jpg",
    "https://images4.alphacoders.com/837/837215.png",
    "https://i.pinimg.com/736x/77/be/4b/77be4b0ea94405a3f51d057aeccba48e.jpg",
    "https://mfiles.alphacoders.com/737/737878.png",
    "https://get.wallhere.com/photo/illustration-anime-cartoon-comics-Boku-no-Hero-Academia-screenshot-comic-book-39907.png"
 ];

  int index = 0;
  int index1 = 1;
  int index2 = 2;
  int index3 = 3;
  int index4 = 4;
  int index5 = 5;

  Container generateContainer( url) {
    return Container(
      padding: const EdgeInsets.all(1),
      child: FlatButton(
        onPressed: () {
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => DisplayImage(imageurl: url)));
        },
        child: Image(
          image: NetworkImage(url),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
          child: GridView.count(
        primary: false,
        padding: const EdgeInsets.all(0),
        crossAxisSpacing: 1,
        mainAxisSpacing: 1,
        crossAxisCount: 2,
        children: <Widget>[
          generateContainer(_allImages[index]),
          generateContainer(_allImages[index2]),
          generateContainer(_allImages[index3]),
          generateContainer(_allImages[index4]),

        ],
      )),
      floatingActionButton: FloatingActionButton(
        onPressed: (){

        },
        child: Icon(Icons.add_a_photo),
      ),
    );
  }
}
