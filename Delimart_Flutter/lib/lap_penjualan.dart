import 'package:flutter/material.dart';
import 'package:flutter_application_1/lap_penjualan_all.dart';
import 'package:flutter_application_1/lap_penjualan_bulanan.dart';
import 'package:flutter_application_1/lap_penjualan_harian.dart';
import 'package:flutter_application_1/lap_penjualan_tahunan.dart'; // Import your login page widget

class LaporanPenjualanPage extends StatefulWidget {
  const LaporanPenjualanPage({Key? key}) : super(key: key);

  @override
  _LaporanPenjualanPageState createState() => _LaporanPenjualanPageState();
}

class _LaporanPenjualanPageState extends State<LaporanPenjualanPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 2, 10, 74),
        title: const Text(
          "Laporan Penjualan",
          style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
        ),
        iconTheme: const IconThemeData(
          color: Colors.white, // Change your color here
        ),
      ),
      
      body: ListView(
        padding: const EdgeInsets.all(25),
        children: <Widget>[
          _buildCard(Icons.trending_up, "Penjualan Hari Ini", const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenjualanHarianPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penjualan Bulan Ini", const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenjualanBulananPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penjualan Tahun Ini", const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenjualanTahunanPage()),
            );
          }),
          _buildCard(Icons.trending_up, "Penjualan Selama Ini", const Color.fromARGB(255, 2, 10, 74), () {
            Navigator.push(
              context,
              MaterialPageRoute(builder: (context) => LaporanPenjualanAllPage()),
            );
          }),
        ],
      ),
    );
  }

  Widget _buildCard(IconData icon, String title, Color iconColor, VoidCallback onTap) {
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
              Text(title, style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold)),
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
