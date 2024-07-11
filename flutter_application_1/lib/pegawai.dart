import 'package:flutter/material.dart';
import 'dart:convert' as convert;
import 'package:http/http.dart' as http;
import 'package:flutter_application_1/navbar.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';

class Pegawai {
  final String kdPegawai;
  final String namaPegawai;
  final String tanggalLahir;
  final String jenisKelamin;
  final String alamat;
  final String telepon;
  final String kdRole;

  Pegawai({
    required this.kdPegawai,
    required this.namaPegawai,
    required this.tanggalLahir,
    required this.jenisKelamin,
    required this.alamat,
    required this.telepon,
    required this.kdRole,
  });

  factory Pegawai.fromJson(Map<String, dynamic> json) {
    return Pegawai(
      kdPegawai: json['kd_pegawai'],
      namaPegawai: json['nama_pegawai'],
      tanggalLahir: json['tanggal_lahir'],
      jenisKelamin: json['jenis_kelamin'],
      alamat: json['alamat'],
      telepon: json['telepon'],
      kdRole: json['role'],
    );
  }
}

class ApiService {
  final String baseUrl = 'https://praktikum-cpanel-unbin.com/golang_api/delimart_api/';

  Future<List<Pegawai>> fetchFromServer() async {
    var url = "$baseUrl/pegawai";
    var response = await http.get(Uri.parse(url));

    List<Pegawai> PegawaiList = [];
    if (response.statusCode == 200) {
      var body = convert.jsonDecode(response.body);
      for (final item in body) {
        PegawaiList.add(Pegawai.fromJson(item));
      }
    } else {
      throw Exception('Failed to load Pegawai');
    }

    return PegawaiList;
  }
}

// Main Application
void main() {
  runApp(PegawaiPage());
}

class PegawaiPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Delimart App',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: PegawaiList(),
    );
  }
}

class PegawaiList extends StatefulWidget {
  @override
  _PegawaiListState createState() => _PegawaiListState();
}

class _PegawaiListState extends State<PegawaiList> {
  late Future<List<Pegawai>> futurePegawai;

  @override
  void initState() {
    super.initState();
    futurePegawai = ApiService().fetchFromServer();
  }

  Future<void> _refreshPegawai() async {
    setState(() {
      futurePegawai = ApiService().fetchFromServer();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Data Pegawai",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white,
        ),
      ),
      drawer: const NavBar(),
      body: LiquidPullToRefresh(
        onRefresh: _refreshPegawai,
        color: Color.fromARGB(255, 2, 10, 74),
        height: 200,
        backgroundColor: Colors.white,
        animSpeedFactor: 1,
        showChildOpacityTransition: false,
        child: FutureBuilder<List<Pegawai>>(
          future: futurePegawai,
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
                  final Pegawai pegawai = snapshot.data![index];
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
                        pegawai.namaPegawai,
                        style: TextStyle(fontWeight: FontWeight.bold),
                      ),
                      subtitle: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text('Jenis Kelamin: ${pegawai.jenisKelamin}'),
                          Text('Tanggal Lahir: ${pegawai.tanggalLahir}'),
                          Text('Alamat: ${pegawai.alamat}'),
                          Text('Contact: ${pegawai.telepon}'),
                          Text('Jabatan: ${pegawai.kdRole}'),
                        ],
                      ),
                    ),
                  );
                },
              );
            }
          },
        ),
      ),
    );
  }
}
