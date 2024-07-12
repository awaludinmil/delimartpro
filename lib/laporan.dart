import 'package:flutter/material.dart';
import 'package:flutter_application_1/lap_arus_stok.dart';
import 'package:flutter_application_1/lap_penjualan.dart'; // Import your login page widget
import 'package:flutter_application_1/lap_penerimaan.dart'; // Import your login page widget
import 'package:flutter_application_1/lap_penjualan_barang.dart';
import 'navbar.dart';

class LaporanPage extends StatefulWidget {
  const LaporanPage({Key? key}) : super(key: key);

  @override
  _LaporanPageState createState() => _LaporanPageState();
}

class _LaporanPageState extends State<LaporanPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Laporan",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
      ),
      drawer: NavBar(),
      body: ListView(
        padding: const EdgeInsets.all(25),
        children: <Widget>[
          _buildCard(Icons.receipt_long, "Laporan Penjualan",
              const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenjualanPage()),
            );
          }),
          _buildCard(Icons.receipt_long, "Laporan Penerimaan",
              const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenerimaanPage()),
            );
          }),
          _buildCard(Icons.receipt_long, "Laporan Penjualan Per Barang",
              const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => LaporanPenjualanBarangPage()),
            );
          }),
          _buildCard(Icons.receipt_long, "Laporan Arus Stok",
              const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanArusStokPage()),
            );
          }),
        ],
      ),
    );
  }

  Widget _buildCard(
      IconData icon, String title, Color iconColor, VoidCallback onTap) {
    return GestureDetector(
      onTap: onTap, // Call the provided onTap function
      child: Card(
        shape: RoundedRectangleBorder(
          side: const BorderSide(
            color: Color.fromARGB(255, 2, 10, 74),
            width: 2,
          ),
          borderRadius: BorderRadius.circular(10),
        ),
        margin: const EdgeInsets.all(8),
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: <Widget>[
              Icon(
                icon,
                size: 30,
                color: iconColor,
              ),
              SizedBox(width: 10),
              Text(title,
                  style:
                      TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold)),
              Spacer(), // Optional: Adds space between title and arrow icon
              Icon(
                Icons.arrow_forward_ios,
                size: 20,
                color: const Color.fromARGB(255, 2, 10, 74),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
