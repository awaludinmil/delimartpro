@extends('layouts.app')

@section('title', 'Form Tambah Supplier')

@section('contents')
  <form action="{{ route('supplier.tambah.simpan') }}" method="post">
    @csrf
    <div class="row">
      <div class="col-12">
        <div class="card shadow mb-4">
          <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Form Tambah Supplier</h6>
          </div>
          <div class="card-body">
            <div class="form-group">
              <label for="kd_supplier">Kode Supplier</label>
              <input type="text" class="form-control" id="kd_supplier" name="kd_supplier">
            </div>
            <div class="form-group">
              <label for="nama">Nama Kategori</label>
              <input type="text" class="form-control" id="nama" name="nama">
            </div>
            <div class="form-group">
              <label for="alamat">Alamat</label>
              <input type="text" class="form-control" id="alamat" name="alamat">
            </div>
            <div class="form-group">
              <label for="telepon">Telepon</label>
              <input type="text" class="form-control" id="telepon" name="telepon">
            </div>
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-primary">Simpan</button>
            <a href="{{ route('supplier') }}" class="btn btn-secondary">Batal</a>
          </div>
        </div>
      </div>
    </div>
  </form>
@endsection
