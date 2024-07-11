@extends('layouts.app')

@section('title', 'Form Pulsa')

@section('contents')
  <form action="{{ route('pulsa.tambah.simpan') }}" method="post">
    @csrf
    <div class="row">
      <div class="col-12">
        <div class="card shadow mb-4">
          <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Form Tambah Pulsa</h6>
          </div>
          <div class="card-body">
            <div class="form-group">
              <label for="kd_pulsa">Kode Pulsa</label>
              <input type="text" class="form-control" id="kd_pulsa" name="kd_pulsa" >
            </div>
            <div class="form-group">
              <label for="nama_produk_pulsa">Produk Pulsa</label>
              <input type="text" class="form-control" id="nama_produk_pulsa" name="nama_produk_pulsa">
            </div>
            <div class="form-group">
              <label for="kd_provider">Provider</label>
              <select name="kd_provider" id="kd_provider" class="custom-select">
                <option value="" selected disabled hidden>-- Pilih Provider --</option>
                @foreach ($provider as $row)
                  <option value="{{ $row['kd_provider'] }}">{{ $row['provider'] }}</option>
                @endforeach
              </select>
            </div>
            <div class="form-group">
              <label for="modal">Modal</label>
              <input type="text" class="form-control" id="modal" name="modal">
            </div>
            <div class="form-group">
              <label for="harga">Harga</label>
              <input type="text" class="form-control" id="harga" name="harga">
            </div>
        </div>
        <div class="card-footer">
            <button type="submit" class="btn btn-primary">Simpan</button>
            <a href="{{ route('pulsa') }}" class="btn btn-secondary">Batal</a>
        </div>
      </div>
    </div>
    </div>
  </form>
@endsection
