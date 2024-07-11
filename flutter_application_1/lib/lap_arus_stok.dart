import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'laporan.dart'; // Import LaporanPage from laporan.dart
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
class LaporanArusStok {
  final String namaBarang;
  final String kdBarang;
  final int masuk;
  final int keluar;

  LaporanArusStok({
    required this.namaBarang,
    required this.kdBarang,
    required this.masuk,
    required this.keluar,
  });

  factory LaporanArusStok.fromJson(Map<String, dynamic> json) {
    return LaporanArusStok(
      namaBarang: json['nama_barang'],
      kdBarang: json['kd_barang'],
      masuk: int.parse(json['barang_masuk']),
      keluar: int.parse(json['barang_keluar']),
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<LaporanArusStok>> fetchFromServer() async {
    var url = "$baseUrl/laporan_arus_stok";
    var response = await http.get(Uri.parse(url));

    List<LaporanArusStok> LaporanArusStokList = [];
    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      for (final item in body) {
        LaporanArusStokList.add(LaporanArusStok.fromJson(item));
      }
    } else {
      throw Exception('Failed to load LaporanArusStok');
    }

    return LaporanArusStokList;
  }
}

// Main Application
void main() {
  runApp(LaporanArusStokPage());
}
 
class LaporanArusStokPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LaporanArusStokList(),
    );
  }
}

class LaporanArusStokList extends StatefulWidget {
  @override
  _LaporanArusStokListState createState() => _LaporanArusStokListState();
}

class _LaporanArusStokListState extends State<LaporanArusStokList> {
  late Future<List<LaporanArusStok>> futureLaporanArusStok;

  @override
  void initState() {
    super.initState();
    futureLaporanArusStok = ApiService().fetchFromServer();
  }
 Future<void> _laporan() async {
    setState(() {
      futureLaporanArusStok = ApiService().fetchFromServer();
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Laporan Arus Stok",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
        leading: IconButton(
          icon: Icon(Icons.arrow_back),
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPage()),
            );
          },
        ),
      ),
      body: LiquidPullToRefresh(
        onRefresh: _laporan,
        color:  Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor:  Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child: FutureBuilder<List<LaporanArusStok>>(
        future: futureLaporanArusStok,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(child: Text('No data found'));
          } else {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                final LaporanArusStok laporan = snapshot.data![index];
                return Card(
                shape: RoundedRectangleBorder(
                    side: const BorderSide(
                      color: Color.fromARGB(255, 2, 10, 74),
                      width: 2,
                    ),
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: ListTile(
                    title: Text(
                      laporan.namaBarang.toUpperCase(),
                      style: TextStyle(fontWeight: FontWeight.bold),
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Kode Barang: ${laporan.kdBarang}'),
                        Text('Masuk: ${laporan.masuk}'),
                        Text('Keluar: ${laporan.keluar}'),
                      ],
                    ),
                  ),
                );
              },
            );
          }
        },
      ),
      )
    );
  }
}
